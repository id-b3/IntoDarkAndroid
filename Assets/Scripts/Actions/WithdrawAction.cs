using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithdrawAction : BaseAction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string GetActionName()
    {
        return "Withdraw";
    }

    public override int GetAPCost()
    {
        return 2;
    }
}
