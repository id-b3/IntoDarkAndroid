using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseAction : MonoBehaviour
{
    protected Operative op;
    protected bool isActive;
    protected Action onActionComplete;

    protected virtual void Awake(){
        op = GetComponent<Operative>();
        SetInactive();
    }

    public abstract string GetActionName();

    public virtual void SetActive(){
        isActive = true;
        Debug.Log("Action Active");
    }

    public virtual void SetInactive(){
        isActive = false;
        Debug.Log("Action Inactive");
    }
}
