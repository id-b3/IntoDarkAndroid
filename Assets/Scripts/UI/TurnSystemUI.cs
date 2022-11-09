using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endActivationButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;


    private void Start(){
        endActivationButton.onClick.AddListener(() => {
            TurnSystem.Instance.FinishActivation();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        UpdateTurnText();
    }

    private void UpdateTurnText(){
        if (TurnSystem.Instance.TurnNumber < 5)
        {
            turnNumberText.text = "TURN " + TurnSystem.Instance.TurnNumber;
        }
        else
        {
            turnNumberText.text = "Game Over";
        }
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e){
        UpdateTurnText();
    }
}
