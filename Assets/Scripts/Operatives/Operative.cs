using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operative : MonoBehaviour
{

    //Actions
    public MoveAction moveAction { get; private set; }
    public PassAction passAction { get; private set; }
    private BaseAction[] baseActionArray;

    private Rigidbody2D opBase;

    // Stats
    public int moveSpeed { get; private set; } = 3;
    public int actionPointLimit { get; private set; } = 2;
    public int modAPL { get; private set; }
    [SerializeField] private int groupActivation;
    [SerializeField] private int defence;
    [SerializeField] private int normSave;
    [SerializeField] private int invulnSave;
    public int wounds { get; private set; } = 10;
    [SerializeField] private float baseSize;

    // While Active vars
    public int ActionPoints { get; private set; }

    // Events
    public static event EventHandler OnAnyActionPointsChanged;

    void Awake()
    {
        opBase = GetComponent<Rigidbody2D>();
        moveAction = GetComponent<MoveAction>();
        passAction = GetComponent<PassAction>();

        baseActionArray = GetComponents<BaseAction>();
        modAPL = actionPointLimit;
        ActionPoints = modAPL;
    }

    void Start()
    {
        transform.localScale = new Vector3(baseSize, baseSize, 0);

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    void Update()
    {

    }

    //Getters
    public Rigidbody2D GetOpBase()
    {
        return opBase;
    }

    public int GetMoveSpeed()
    {
        return moveSpeed;
    }

    public BaseAction[] GetBaseActions()
    {
        return baseActionArray;
    }

    //Setters
    public void UseActionPoints(int apMod)
    {
        int newAP = ActionPoints - apMod;
        if (newAP >= 0)
        {
            ActionPoints = newAP;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ModifyAPL(bool increase = true)
    {
        if (modAPL != actionPointLimit) return;
        else
        {
            if (increase) modAPL++;
            else modAPL--;
        }
    }


    public bool CanTakeAction(BaseAction selectedAction)
    {
        return ActionPoints >= selectedAction.GetAPCost();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        ActionPoints = modAPL;
        modAPL = actionPointLimit;
        moveAction.ResetMovement();
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);

    }

}
