using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Operative op;
    protected bool isActive;
    protected Action onActionComplete;

    protected virtual void Awake(){
        op = GetComponent<Operative>();
        isActive = false;
    }

    public abstract string GetActionName();
}
