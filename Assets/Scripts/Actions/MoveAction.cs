using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
   
    public delegate void MovingDelegate();

    private DistanceJoint2D limiter;

    private float opMoveDist;
    private float distMoved;

    private Vector3 startingPosition;
    private Vector3 offset;


    protected override void Awake(){
        base.Awake();
        opMoveDist = op.GetMoveSpeed() * Constants.inch * 2;
        limiter = GetComponent<DistanceJoint2D>();
    }

    void Update(){
        
    }

    public void MoveOperative(Vector3 pos){
        if (!isActive) return;
        
        Vector2 distance = limiter.connectedAnchor - (Vector2)MouseWorld.GetPosition();
        distMoved = distance.magnitude;

        if (distMoved > opMoveDist) return;
        else if (opMoveDist == 0) return;
        else op.GetOpBase().MovePosition(pos + offset);
    }

    public void ManageLimitingJoint(bool enableJoint){
        if (enableJoint){
            limiter.connectedAnchor = op.transform.position;
            limiter.distance = opMoveDist;
            limiter.enabled = true;
        }
        else limiter.enabled = false;
    }

    public void SetStartPos(){       
        startingPosition = op.transform.position;
        offset = startingPosition - MouseWorld.GetPosition();
    }
    
    public void ChangeRemainingMove(){
        if (opMoveDist  > 0){
            float inchesMoved = (startingPosition - op.transform.position).magnitude / Constants.inch;
            inchesMoved = Mathf.Ceil(inchesMoved);
            Debug.Log("Inches Moved: " + inchesMoved);
            opMoveDist -= (inchesMoved * Constants.inch);
            Debug.Log("Movement Left: " + opMoveDist);
        }
    }

    public override string GetActionName(){
        return "Move";
    }
}