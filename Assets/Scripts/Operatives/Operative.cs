using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operative : MonoBehaviour
{

    //Actions
    private MoveAction moveAction;
    private BaseAction[] baseActionArray;

    private Rigidbody2D opBase;
    [SerializeField] private int moveSpeed;

    void Awake()
    {
        opBase = GetComponent<Rigidbody2D>();
        moveAction = GetComponent<MoveAction>();
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

    public int GetMoveSpeed(){
        return moveSpeed;
    }

    public BaseAction[] GetBaseActions(){
        return baseActionArray;
    }

}
