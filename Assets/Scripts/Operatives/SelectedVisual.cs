using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisual : MonoBehaviour
{
    [SerializeField] private Operative operative;
    private SpriteRenderer spriteRenderer;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OpActionSystem.Instance.OnSelectedOpChanged += OperativeActionSystem_OnSelectOpChange;
        UpdateVisual();
    }

    private void OperativeActionSystem_OnSelectOpChange(object sender, EventArgs empty){
        UpdateVisual();
    }

    private void UpdateVisual(){
        if (OpActionSystem.Instance.OpIsSelected() & operative == OpActionSystem.Instance.GetSelectedOp()){
            spriteRenderer.enabled = true;
        }
        else spriteRenderer.enabled = false;
    }
}

