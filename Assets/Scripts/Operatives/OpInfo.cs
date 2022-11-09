using System.Collections.Generic;

public class OpInfo
{
    public string OpName { get; private set; }
    public int M { get; private set; }
    public int APL { get; private set; }
    public int GA { get; private set; }
    public int DF { get; private set; }
    public int S { get; private set; }
    public int W { get; private set; }
    public float B { get; private set; }
    
    public List<Weapon> opWeaponList { get; private set; }

    public OpInfo(string name, int m, int apl, int ga, int df, int s, int w, float b, List<Weapon> weapons)
    {
        OpName = name;
        M = m;
        APL = apl;
        GA = ga;
        DF = df;
        S = s;
        W = w;
        B = b;
        opWeaponList = weapons;
    }
}