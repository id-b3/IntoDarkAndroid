using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<int> RollXd6(int x)
    {
        if (x < 1) return null;

        List<int> results = new();
        for (int i = 0; i < x; i++)
        {
            results.Add(Random.Range(1, 7));
        }
        return results;
    }
}
