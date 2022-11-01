using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassAction : BaseAction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive){
            if (Input.GetMouseButtonDown(1)){
                op.UseActionPoints(GetAPCost());
            }
        }
    }

    public override string GetActionName()
    {
        return "Pass";
    }
}
