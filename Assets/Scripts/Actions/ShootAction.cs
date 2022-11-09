using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{

    public void ShootWeapon(Weapon weapon)
    {
        if (weapon.Ranged)
        {
            for (int i = 0; i < weapon.Attacks; i++)
            {
                string hit = "Miss!";
                int d6 = Random.Range(1, 7);
                if (d6 >= weapon.Skill) { hit = "Hit!"; }
                Debug.Log("Attack " + i + " " + d6 + " " + hit);
            }
        }
    }

    public override string GetActionName()
    {
        return "Shoot";
    }
}
