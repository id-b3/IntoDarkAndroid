using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive & Input.GetMouseButtonDown(1)){
            op.ModifyAPL(true);
        }
        
    }

    public override string GetActionName()
    {
        return "Shoot";
    }
}
