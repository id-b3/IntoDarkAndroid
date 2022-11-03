using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MoveAction : BaseAction
{

    public delegate void MovingDelegate();

    private DistanceJoint2D limiter;

    private float opMoveDist;
    private float distMoved;

    private Vector3 originalPosition;
    private Vector3 startingPosition;
    private Vector3 offset;

    private bool moving;


    protected override void Awake()
    {
        base.Awake();
        opMoveDist = op.GetMoveSpeed() * Constants.inch * 2;
        limiter = GetComponent<DistanceJoint2D>();
        moving = false;
    }

    void Update()
    {

        if (isActive)
        {
            if (Touchscreen.current.primaryTouch.IsPressed())
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                if (!moving)
                {
                    SetStartPos();
                    ManageLimitingJoint(true);
                    moving = true;
                }

            }
            if (Input.GetMouseButton(0))
            {
                if (moving) MoveOperative(new Vector3(0,0,0)); //MouseWorld
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (moving)
                {
                    ManageLimitingJoint(false);
                    ChangeRemainingMove(op.transform.position);
                }
                moving = false;
            }
        }

    }

    public void MoveOperative(Vector2 pos)
    {
        Vector2 distance = (Vector2) startingPosition - pos;
        distMoved = distance.magnitude;

        if (distMoved > opMoveDist) return;
        else if (opMoveDist == 0) return;
        else op.GetOpBase().MovePosition(pos + (Vector2)offset);
    }

    public void ManageLimitingJoint(bool enableJoint)
    {
        if (enableJoint)
        {
            limiter.connectedAnchor = op.transform.position;
            limiter.distance = opMoveDist;
            limiter.enabled = enableJoint;
        }
        else limiter.enabled = enableJoint;
    }

    public void SetStartPos()
    {
        startingPosition = op.transform.position;
        offset = startingPosition - new Vector3(0,0,0); //MouseWorld.GetTouchPosition();
    }

    public void ChangeRemainingMove(Vector3 endPos)
    {
        if (opMoveDist > 0)
        {
            float inchesMoved = (startingPosition - endPos).magnitude / Constants.inch;
            inchesMoved = Mathf.Ceil(inchesMoved);
            Debug.Log("Inches Moved: " + inchesMoved);
            opMoveDist -= (inchesMoved * Constants.inch);
            Debug.Log("Movement Left: " + opMoveDist);
        }
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public void ResetMovement(){
        opMoveDist = op.GetMoveSpeed() * Constants.inch * 2;
    }

    public override void SetActive()
    {
        originalPosition = op.transform.position;
        base.SetActive();
    }
}