using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Lean.Touch;

public class MoveAction : BaseAction
{

    public delegate void MovingDelegate();

    [SerializeField] private LeanDragTranslateRigidbody2D dragger;

    private DistanceJoint2D limiter;

    private float opMoveDist;
    private float distMoved;

    private Vector3 originalPosition;
    private Vector3 startingPosition;

    //private bool moving;

    protected override void Awake()
    {
        base.Awake();
        
        limiter = GetComponent<DistanceJoint2D>();
        //moving = false;
    }

    private void Start()
    {
        SetMovement();
    }

    private void SetMovement()
    {
        int baseMove = op.Injured ? op.GetMoveSpeed() - 1 : op.GetMoveSpeed();
        opMoveDist = baseMove * Constants.inch * 2;
    }

    void Update()
    {
    }

    public void ManageLimitingJoint(bool enableJoint)
    {
        if (!isActive) return;
        Debug.Log("Managing the limiter " + enableJoint);
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
        Debug.Log("Starting Position op this move " + op.transform.position);
        startingPosition = op.transform.position;
    }

    public void ChangeRemainingMove()
    {
        if (opMoveDist > 0 & isActive)
        {
            float inchesMoved = (startingPosition - op.transform.position).magnitude / Constants.inch;
            inchesMoved = Mathf.Ceil(inchesMoved);
            Debug.Log("Inches Moved: " + inchesMoved);
            opMoveDist -= (inchesMoved * Constants.inch);
            Debug.Log("Movement Left: " + opMoveDist);


            if (opMoveDist == 0)
            {
                dragger.enabled = false;
                op.UseActionPoints(1);
            }
        }
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public void ResetMovement(){
        SetMovement();
        if (isActive)
        {
            dragger.enabled = true;
        }
    }

    public override void SetActive()
    {
        Debug.Log("Starting Position " + op + " " + op.transform.position);
        originalPosition = op.transform.position;
        base.SetActive();
        dragger.enabled = true;
    }

    public override void SetInactive()
    {
        base.SetInactive();
        dragger.enabled = false;
    }
}