using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisual : MonoBehaviour
{
    [SerializeField] private Operative operative;
    [SerializeField] private Sprite player1;
    [SerializeField] private Sprite player2;
    [SerializeField] private Sprite player3;

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
            switch (operative.OwningPlayerId)
            {
                case 1:
                    spriteRenderer.sprite = player1;
                    break;
                case 2:
                    spriteRenderer.sprite = player2;
                    break;
                case 3:
                    spriteRenderer.sprite = player3;
                    break;
                default:
                    spriteRenderer.sprite = player1;
                    break;
            }
            spriteRenderer.enabled = true;
        }
        else spriteRenderer.enabled = false;
    }
}

