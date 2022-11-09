using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    public int PrimaryVP { get; private set; } = 0;
    public int SecondaryVP { get; private set; } = 0;
    public int CommandP { get; private set; }
    private List<Operative> playersOps;
    public string PlayerName { get; private set; }
    public bool Initiative { get; private set; }
    public int PlayerIndex { get; private set; }
    // Start is called before the first frame update

    public Player(string name, int idx)
    {
        PrimaryVP = 0;
        SecondaryVP = 0;
        CommandP = 2;
        PlayerName = name;
        Initiative = false;
        PlayerIndex = idx;
    }
    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }

    public void GiveInitiative(bool initiative)
    {
        Initiative = initiative;
    }
}
