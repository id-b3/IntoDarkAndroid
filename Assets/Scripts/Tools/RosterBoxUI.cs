using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RosterBoxUI : MonoBehaviour
{
    [SerializeField] Transform spawnerButtonPrefab;
    [SerializeField] Transform spawnerButtonContainer;

    private List<OpInfo> opStatsList;
    private List<Weapon> opWeapons;
    // Start is called before the first frame update
    void Start()
    {
        Weapon gSpearR = new Weapon(true, "Guardian Spear", 4, 2, 3, 5);
        Weapon gSpearM = new Weapon(false, "Guardian Spear", 5, 2, 5, 7);
        opWeapons = new List<Weapon> { gSpearR, gSpearM };

        OpInfo opInfo1 = new OpInfo("MrLeader", 3, 3, 1, 3, 2, 19, 4, opWeapons);
        OpInfo opInfo2 = new OpInfo("Banana Phone", 3, 3, 1, 3, 2, 18, 4, opWeapons);
        OpInfo opInfo3 = new OpInfo("Ayayayayaa", 3, 3, 1, 3, 2, 18, 4, opWeapons);
        OpInfo opInfo4 = new OpInfo("Oil Can", 3, 3, 1, 3, 2, 18, 4, opWeapons); 
        opStatsList = new List<OpInfo> {
            opInfo1, opInfo2, opInfo3, opInfo4
        };
    

        int index = 0;
        foreach (OpInfo opStat in opStatsList)
        {
            Vector3 parPos = spawnerButtonContainer.transform.position;
            Vector3 butPos = new(parPos.x + index*70 + 20, parPos.y - (Mathf.Floor(index/3)*20) - 20, 0);
            index++;
            Transform button = Instantiate(spawnerButtonPrefab, butPos, Quaternion.identity, spawnerButtonContainer);
            OpSpawnerButtonUI buttonSpawner = button.GetComponent<OpSpawnerButtonUI>();
            buttonSpawner.SetOpStats(opStat);
            buttonSpawner.GetComponentInChildren<TextMeshProUGUI>().text = opStat.OpName;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
