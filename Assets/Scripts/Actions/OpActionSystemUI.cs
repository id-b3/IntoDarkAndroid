using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpActionSystemUI : MonoBehaviour
{

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainer;

    // Start is called before the first frame update
    void Start()
    {
        OpActionSystem.Instance.OnSelectedOpChanged += OpActionSystem_OnSelectedOpChanged;
        CreateOpActionButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateOpActionButtons(){
        foreach (Transform buttonTransform in actionButtonContainer){
            Destroy(buttonTransform.gameObject);
        }
        Operative op = OpActionSystem.Instance.GetSelectedOp();
        foreach (BaseAction action in op.GetBaseActions()){
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainer);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(action);
        }
    }

    private void OpActionSystem_OnSelectedOpChanged(object sender, EventArgs e){
        CreateOpActionButtons();
    }
}
