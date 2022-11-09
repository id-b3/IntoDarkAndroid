using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class OpSelectionSystem : MonoBehaviour
{
    public static OpSelectionSystem Instance;
    [SerializeField] LeanSelectByFinger selectionTool;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Turn System! " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    public void Start()
    {
        Operative.OnAnyOpActivated += Operative_OnAnyOpActivated; ;
    }

    private void Operative_OnAnyOpActivated(object sender, System.EventArgs e)
    {
        //Debug.Log("Operative Selection System - OP Activated");
        //if (OpActionSystem.Instance.GetSelectedOp().Activated)
        //{
        //    selectionTool.enabled = false;
        //}
    }

    public void EnableOpSelection(bool enable)
    {
        //selectionTool.enabled = enable;
    }
}
