using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedIndicator;
    [SerializeField] private GameObject toggleExtraButtons;

    private BaseAction baseAction;

    public void SetBaseAction(BaseAction action){
        this.baseAction = action;
        textMeshPro.text = action.GetActionName().ToUpper();
        button.onClick.AddListener(() => {
            OpActionSystem.Instance.SetSelectedAction(action);
        });
    }

    public void UpdateSelectedVisual(){
        BaseAction selectedBaseAction = OpActionSystem.Instance.GetSelectedAction();
        selectedIndicator.SetActive(selectedBaseAction == baseAction);
        toggleExtraButtons.SetActive(selectedBaseAction == baseAction);
    }
}
