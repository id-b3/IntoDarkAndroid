using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class Operative : MonoBehaviour
{

    //Actions
    public MoveAction moveAction { get; private set; }
    public PassAction passAction { get; private set; }
    public ShootAction shootAction { get; private set; }

    private BaseAction[] baseActionArray;

    [SerializeField] private CircleCollider2D engagementRange;
    [SerializeField] private LeanSelectableByFinger selectable;
    private Rigidbody2D opBase;

    // Stats
    [SerializeField] private int moveSpeed;
    [SerializeField] private int ActionPointLimit;
    [SerializeField] private int groupActivation;
    [SerializeField] private int defence;
    [SerializeField] private int normSave;
    [SerializeField] private int invulnSave;
    [SerializeField] private int maxWounds;
    [SerializeField] private string opName;
    [SerializeField] private float baseSize;
    [SerializeField] private bool canBeInjured = true;

    //Weapons
    public List<Weapon> opWeapons { get; private set; }

    // While Active vars
    public int CurrentWounds { get; private set; }
    public int ActionPoints { get; private set; }
    public int ModAPL { get; private set; }
    public bool Incapacitated { get; private set; } = false;
    public bool Injured { get; private set; } = false;
    public bool Activated { get; private set; } = false;
    public int Team { get; private set; }

    public int OwningPlayerId { get; private set; }

    // Events
    public static event EventHandler OnAnyActionPointsChanged;
    public static event EventHandler OnAnyOpActivated;

    void Awake()
    {
        opBase = GetComponent<Rigidbody2D>();
        moveAction = GetComponent<MoveAction>();
        passAction = GetComponent<PassAction>();
        shootAction = GetComponent<ShootAction>();
        

        baseActionArray = GetComponents<BaseAction>();
    }

    void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        TurnSystem.Instance.OnActivationDone += TurnSystem_OnActivationDone;
    }

    private void TurnSystem_OnActivationDone(object sender, EventArgs e)
    {
        SetSelectable();
    }

    private void SetSelectable()
    {
        if (TurnSystem.Instance.ActivatedPlayer.PlayerIndex == OwningPlayerId & !Activated)
        {
            selectable.enabled = true;
            Debug.Log("Player ID " + OwningPlayerId);
            Debug.Log(opName);
        }
        else
        {
            selectable.enabled = false;
        }
    }

    public void InitOp(string name, int m, int apl, int ga, int df, int s, int w, float b, List<Weapon> weapons)
    {
        moveSpeed = m;
        ActionPointLimit = apl;
        groupActivation = ga;
        defence = df;
        normSave = s;
        maxWounds = w;
        baseSize = b;

        opName = name;

        opWeapons = weapons;

        transform.localScale = new Vector3(baseSize, baseSize, 0);
        engagementRange.transform.localScale = new Vector3(1 / baseSize, 1 / baseSize, 0);
        engagementRange.radius = (baseSize / 2) + Constants.inch;
        CurrentWounds = maxWounds;
        ModAPL = ActionPointLimit;
        ActionPoints = ModAPL;
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
        ActionPoints = Mathf.Clamp(ActionPoints - apMod, 0, ActionPointLimit);
        if (ActionPoints < ModAPL)
        {
            SetActivated(true);
        }
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ModifyAPL(bool increase = true)
    {
        if (ModAPL != ActionPointLimit) return;
        else
        {
            if (increase) ModAPL++;
            else ModAPL--;
        }
    }

    public void ResetAPL()
    {
        ModAPL = ActionPointLimit;
    }

    public bool CanTakeAction(BaseAction selectedAction)
    {
        return ActionPoints >= selectedAction.GetAPCost();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        ActionPoints = ModAPL;
        moveAction.ResetMovement();
        Activated = false;
        SetSelectable();
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ModifyWounds(int dam, bool damage = true)
    {
        if (damage) dam *= -1;
        CurrentWounds = Mathf.Clamp(CurrentWounds + dam, 0, maxWounds);
        Debug.Log("Operative Wounds Left: " + CurrentWounds);
        SetInjured(false);
        SetIncapacitated();
    }

    private void SetInjured(bool forceInjured)
    {
        if (canBeInjured & (CurrentWounds < (maxWounds / 2) | forceInjured))
        {
            Injured = true;
        }
        else
        {
            Injured = false;
        }
    }

    public void SetIncapacitated()
    {
        if (CurrentWounds <= 0)
        {
            Incapacitated = true;
            UseActionPoints(8);
            OpActionSystem.Instance.DeselectAllOps();
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder -= 1;
            Debug.Log("Operative is Incapacitated: " + Incapacitated);
        }
    }

    public void SetActivated(bool activated)
    {
        Activated = activated;
        OnAnyOpActivated?.Invoke(this, EventArgs.Empty);
    }

    public void SetOwnerPlayer(int playerIdx)
    {
        OwningPlayerId = playerIdx;
        Debug.Log("Owner Player Index " + playerIdx);
    }

    public string GetOpName()
    {
        return opName;
    }

}
