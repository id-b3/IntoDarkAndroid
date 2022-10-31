using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operative : MonoBehaviour
{

    //Actions
    private MoveAction moveAction;
    private MeasureAction measureAction;
    private BaseAction[] baseActionArray;

    private Rigidbody2D opBase;
    [SerializeField] private int moveSpeed;

    void Awake()
    {
        opBase = GetComponent<Rigidbody2D>();
        moveAction = GetComponent<MoveAction>();
        measureAction = GetComponent<MeasureAction>();
        baseActionArray = GetComponents<BaseAction>();
    }

    void Update()
    {
        
    }

    //Getters
    public Rigidbody2D GetOpBase(){
        return opBase;
    }

    public MoveAction GetMoveAction(){
        return moveAction;
    }

    public MeasureAction GetMeasureAction(){
        return measureAction;
    }

    public int GetMoveSpeed(){
        return moveSpeed;
    }

    public BaseAction[] GetBaseActions(){
        return baseActionArray;
    }

}
