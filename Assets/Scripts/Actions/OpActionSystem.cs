using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpActionSystem : MonoBehaviour
{
    public static OpActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedOpChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler OnActionTaken;

    [SerializeField] private Operative selectedOp;
    [SerializeField] private LayerMask opLayerMask;
    [SerializeField] private MeasuringTape tape;

    private BaseAction selectedAction;

    private bool isBusy;
    private int actionPoints;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Action System! " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        SetSelectedOp(selectedOp);
    }

    void Update()
    {
        if (isBusy) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (TryHandleUnitSelection()) return;
        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (selectedAction)
            {
                case MoveAction moveAction:
                    break;
                case FightAction fightAction:
                    break;
                case MeasureAction measureAction:
                    break;
            }
            OnActionTaken?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPos = new Vector3(0,0,0); // MouseWorld.GetTouchPosition();
            Collider2D target = Physics2D.OverlapPoint(touchPos, opLayerMask);
            if (target)
                if (target.transform.gameObject.TryGetComponent<Operative>(out Operative op))
                {
                    if (selectedOp == op) return false;
                    else
                    {
                        SetSelectedOp(op);
                        return true;
                    }
                }
        }
        return false;
    }

    private void SetSelectedOp(Operative op)
    {
        selectedOp = op;
        SetSelectedAction(op.passAction);
        OnSelectedOpChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        if (selectedAction)
        {
            selectedAction.SetInactive();
        }

        if (selectedOp.CanTakeAction(baseAction))
        {
            selectedAction = baseAction;
            selectedAction.SetActive();

            switch (selectedAction)
            {
                case MeasureAction measureAction:
                    measureAction.SetMeasuringTape(tape);
                    break;
            }
            OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
        }
        else Debug.Log("No AP left for this operative.");
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    public Operative GetSelectedOp()
    {
        return selectedOp;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}
