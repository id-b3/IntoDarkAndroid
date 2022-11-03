using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpActionSystemUI : MonoBehaviour
{

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainer;
    [SerializeField] private TextMeshProUGUI actionPointsText;
    
    private List<ActionButtonUI> actionButtonUIList;

    void Awake(){
        actionButtonUIList = new List<ActionButtonUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OpActionSystem.Instance.OnSelectedOpChanged += OpActionSystem_OnSelectedOpChanged;
        OpActionSystem.Instance.OnSelectedActionChanged += OpActionSystem_OnSelectedActionChanged;
        OpActionSystem.Instance.OnActionTaken += OpActionSystem_OnActionTaken;
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        Operative.OnAnyActionPointsChanged += Operative_OnAnyActionPointsChanged;

        CreateOpActionButtons();
        UpdateSelectedVisual();
        UpdateActionPointsVisual();
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

        if (!OpActionSystem.Instance.OpIsSelected())
        {
            return;
        }
        {
            foreach (BaseAction action in OpActionSystem.Instance.GetSelectedOp().GetBaseActions())
            {
                Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainer);
                ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(action);

                actionButtonUIList.Add(actionButtonUI);
            }
        }
    }

    private void OpActionSystem_OnSelectedOpChanged(object sender, EventArgs e){
        CreateOpActionButtons();
        UpdateSelectedVisual();
        UpdateActionPointsVisual();
    }

    private void OpActionSystem_OnSelectedActionChanged(object sender, EventArgs e){
        UpdateSelectedVisual();
    }

    private void OpActionSystem_OnActionTaken(object sender, EventArgs e){
        UpdateActionPointsVisual();
    }

    private void Operative_OnAnyActionPointsChanged(object sender, EventArgs e){
        UpdateActionPointsVisual();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e){
        UpdateActionPointsVisual();
    }

    private void UpdateSelectedVisual(){
        foreach (ActionButtonUI actionButtonUI in actionButtonUIList){
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPointsVisual(){
        if (!OpActionSystem.Instance.OpIsSelected())
        {
            actionPointsText.enabled = false;
            return;
        }
        int apl = OpActionSystem.Instance.GetSelectedOp().modAPL;
        int apLeft = OpActionSystem.Instance.GetSelectedOp().ActionPoints;
        actionPointsText.text = "AP: " + apLeft + "/" + apl;
        actionPointsText.enabled = true;
    }
}
