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
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        UpdateTurnText();
    }

    private void UpdateTurnText(){
        turnNumberText.text = "TURN " + TurnSystem.Instance.turnNumber;
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e){
        UpdateTurnText();
    }
}
