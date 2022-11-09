public class Weapon
{
    public string WeaponName { get; private set; }
    public int Attacks { get; private set; }
    public int Skill { get; private set; }
    public int DamageNorm { get; private set; }
    public int DamageCrit { get; private set; }
    public int SpecialRule { get; private set; }
    public int CritRule { get; private set; }
    public bool Ranged { get; private set; }

    public Weapon(bool ranged, string name, int A, int S, int DN, int DC)
    {
        Ranged = ranged;
        WeaponName = name;
        Attacks = A;
        Skill = S;
        DamageNorm = DN;
        DamageCrit = DC;
    }

}
