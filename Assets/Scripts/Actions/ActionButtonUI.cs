using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;

    public void SetBaseAction(BaseAction action){
        textMeshPro.text = action.GetActionName().ToUpper();
        button.onClick.AddListener(() => {
            OpActionSystem.Instance.SetSelectedAction(action);
        });
    }


}
