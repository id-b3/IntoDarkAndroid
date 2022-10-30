using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpActionSystem : MonoBehaviour
{
    public static OpActionSystem Instance { get; private set;}
    public event EventHandler OnSelectedOpChanged;
    [SerializeField] private Operative selectedOp; 
    [SerializeField] private LayerMask opLayerMask;
    private MeasuringTape tape;

    private bool isBusy;

    private void Awake()
    {
        if (Instance != null){
            Debug.LogError("More than one Action System! " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        tape = GetComponent<MeasuringTape>();
    }

    void Update()
    {
        if (isBusy) return;
        if (Input.GetMouseButtonDown(0)){
            if (TryHandleUnitSelection()) return;
            // selectedOp.GetMoveAction().ManageLimitingJoint(true);
            // tape.SetStartPos(MouseWorld.GetPosition());
            // selectedOp.GetMoveAction().SetStartPos();
        }

        // if (Input.GetMouseButton(0)){
        //     selectedOp.GetMoveAction().MoveOperative(MouseWorld.GetPosition());
        //     tape.RenderTape();
        // }

        // if (Input.GetMouseButtonUp(0)){
        //     selectedOp.GetMoveAction().ManageLimitingJoint(false);
        //     selectedOp.GetMoveAction().ChangeRemainingMove();
        //     tape.DisableTape();
        // }

    }

    private bool TryHandleUnitSelection(){
        Vector3 mousePos = MouseWorld.GetPosition();
        Collider2D target = Physics2D.OverlapPoint(mousePos, opLayerMask);
        if (target)
        if (target.transform.gameObject.TryGetComponent<Operative>(out Operative op)){
            if (selectedOp == op) return false;
            else{
                SetSelectedOp(op);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedOp(Operative op){
        selectedOp = op;
        OnSelectedOpChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetBusy(){
        isBusy = true;
    }

    private void ClearBusy(){
        isBusy = false;
    }

    public Operative GetSelectedOp(){
        return selectedOp;
    }
}
