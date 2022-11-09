using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;


public class ToolSystemUI : MonoBehaviour
{
    [SerializeField] private Button measuringTapeButton;
    [SerializeField] private TextMeshProUGUI distanceText;


    private void Start()
    {
        measuringTapeButton.onClick.AddListener(() => {
            //ToolSystem.Instance.SetSelectedTool(tool);
        });

        //TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    //private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
   // {
     //   UpdateTurnText();
    //}
}
