using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpActionSystemUI : MonoBehaviour
{

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainer;
    
    private List<ActionButtonUI> actionButtonUIList;

    void Awake(){
        actionButtonUIList = new List<ActionButtonUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OpActionSystem.Instance.OnSelectedOpChanged += OpActionSystem_OnSelectedOpChanged;
        OpActionSystem.Instance.OnSelectedActionChanged += OpActionSystem_OnSelectedActionChanged;
        CreateOpActionButtons();
        UpdateSelectedVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateOpActionButtons(){
        foreach (Transform buttonTransform in actionButtonContainer){
            Destroy(buttonTransform.gameObject);
        }

        actionButtonUIList.Clear();

        Operative op = OpActionSystem.Instance.GetSelectedOp();
        foreach (BaseAction action in op.GetBaseActions()){
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainer);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(action);

            actionButtonUIList.Add(actionButtonUI);
        }
    }

    private void OpActionSystem_OnSelectedOpChanged(object sender, EventArgs e){
        CreateOpActionButtons();
        UpdateSelectedVisual();
    }

    private void OpActionSystem_OnSelectedActionChanged(object sender, EventArgs e){
        UpdateSelectedVisual();
    }

    private void UpdateSelectedVisual(){
        foreach (ActionButtonUI actionButtonUI in actionButtonUIList){
            actionButtonUI.UpdateSelectedVisual();
        }
    }
}
