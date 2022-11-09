using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Lean.Touch;
using Lean.Common;


public class OpActionSystem : MonoBehaviour
{
    public static OpActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedOpChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler OnActionTaken;

    private Operative selectedOp;
    private BaseAction selectedAction;

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
        TurnSystem.Instance.OnTurnChanged += Instance_OnTurnChanged;
        TurnSystem.Instance.OnActivationDone += TurnSystem_OnActivationDone;
        SetSelectedOp(null);
    }

    private void TurnSystem_OnActivationDone(object sender, EventArgs e)
    {
        if (OpIsSelected())
        {
            selectedOp.UseActionPoints(4);
            selectedOp.SetActivated(true);
            selectedOp.ResetAPL();
        }
        DeselectAllOps();
    }

    private void Instance_OnTurnChanged(object sender, EventArgs e)
    {
        DeselectAllOps();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelectedAction();
        }
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0) & selectedAction != null)
        {
            switch (selectedAction)
            {
                case MoveAction moveAction:
                    break;
                case FightAction fightAction:
                    break;
                case ShootAction shootAction:
                    shootAction.ShootWeapon(selectedOp.opWeapons[0]);
                    break;
            }
            OnActionTaken?.Invoke(this, EventArgs.Empty);
        }
    }

    public void TestSelection(LeanSelectable selectable)
    {
        bool success = selectable.TryGetComponent<Operative>(out Operative op);
        Debug.Log("Selected " + success + " " + op);
    }

    public void TryHandleUnitSelection(LeanSelectable selectedOp)
    {
        if (selectedOp.TryGetComponent<Operative>(out Operative op))
        {
            if (selectedOp == op) return;
            SetSelectedOp(op);
        }
        Debug.Log("Unit Selection " + op);
    }

    public void DeselectAllOps()
    {
        if (!OpIsSelected()) return;
        Debug.Log("Deselecting All Ops");
        LeanSelectable opSelected = selectedOp.GetComponent<LeanSelectable>();
        opSelected.Deselect();
        SetSelectedOp(null);
        OpSelectionSystem.Instance.EnableOpSelection(true);
    }

    private void SetSelectedOp(Operative op)
    {
        selectedOp = op;
        if (OpIsSelected())
        {
            SetSelectedAction(op.passAction);
        }
        else
        {
            if (selectedAction != null)
            {
                selectedAction.SetInactive();
                selectedAction = null;
            }
        }
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
                case PassAction passAction:
                    break;
            }
            OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
        }
        else Debug.Log("No AP left for this operative.");
    }

    public Operative GetSelectedOp()
    {
        return selectedOp;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }

    public bool OpIsSelected()
    {
        return selectedOp != null;
    }

    public void DamageSelectedOp(bool heal)
    {
        if (OpIsSelected())
        {
            if (heal)
                selectedOp.ModifyWounds(1, false);
            else
                selectedOp.ModifyWounds(1);
        }
    }
}
