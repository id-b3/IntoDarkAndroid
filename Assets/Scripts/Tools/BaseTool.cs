using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseTool : MonoBehaviour
{
    protected bool IsActive { get; set; } = false;
    protected Action onActionComplete;

    protected virtual void Awake()
    {}

    public abstract string GetActionName();

}
