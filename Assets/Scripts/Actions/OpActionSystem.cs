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


    [SerializeField] private Operative selectedOp;
    [SerializeField] private LayerMask opLayerMask;

    private BaseAction selectedAction;

    [SerializeField] private MeasuringTape tape;

    private bool isBusy;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Action System! " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Debug.Log("Tape is: " + tape.name);
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
                    Debug.Log("Selected Move Action");
                    break;
                case FightAction fightAction:
                    Debug.Log("Selected Fight Action");
                    break;
                case MeasureAction measureAction:
                    Debug.Log("Selected Measure Action");
                    break;
            }
        }
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = MouseWorld.GetPosition();
            Collider2D target = Physics2D.OverlapPoint(mousePos, opLayerMask);
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
        SetSelectedAction(op.GetMeasureAction());
        OnSelectedOpChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        if (selectedAction) {
            selectedAction.SetInactive();
        }
        selectedAction = baseAction;
        selectedAction.SetActive();

        switch(selectedAction)
        {
            case MeasureAction measureAction:
                measureAction.SetMeasuringTape(tape);
                Debug.Log("Set tape measure for measurement action.");
                break;
        }
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
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

    public BaseAction GetSelectedAction(){
        return selectedAction;
    }
}
