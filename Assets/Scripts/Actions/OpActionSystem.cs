using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
        //if (EventSystem.current.IsPointerOverGameObject()) return;
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

    public void TryHandleUnitSelection(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        {
            Debug.Log("Unit Selection");
            Vector3 touchPos = CameraController.Instance.GetPointerPosition();
            Collider2D target = Physics2D.OverlapPoint(touchPos, opLayerMask);
            if (target)
            {
                if (target.transform.gameObject.TryGetComponent<Operative>(out Operative op))
                {
                    if (selectedOp == op) return;
                    else
                    {
                        SetSelectedOp(op);
                        return;
                    }
                }
            }
            Debug.Log("Deselecting All Ops");
            SetSelectedOp(null);

        }
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
            selectedAction.SetInactive();
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
}
