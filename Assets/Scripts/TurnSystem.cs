using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{

    public static TurnSystem Instance { get; private set; }
    public int TurnNumber { get; private set; } = 0;

    // Game Phase: 1 - Setup, 2 - Strategy, 3 - Turn, 4 - Game End
    public int GamePhase { get; private set; } = 1;

    // Handle Activations
    private GameObject[] allOperatives;
    private List<Operative> activeOperatives;

    public event EventHandler OnTurnChanged;
    public event EventHandler OnActivationDone;

    // Handle Players
    private List<Player> players;
    public Player ActivatedPlayer { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Turn System! " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        GamePhase = 1;
    }

    private void Start()
    {
        Player p1 = new("Player1", 1);
        p1.GiveInitiative(true);
        Player p2 = new("Player2", 2);
        players = new List<Player> { p1, p2 };
        ResetActiveOperativeList();
        ActivatedPlayer = p1;
        Debug.Log("Current Player Activation " + ActivatedPlayer.PlayerName);
    }

    private void ResetActiveOperativeList()
    {
        allOperatives = GameObject.FindGameObjectsWithTag("Operative");
        activeOperatives = new List<Operative>();
        foreach (GameObject obj in allOperatives)
        {
            Operative op = obj.GetComponent<Operative>();
            if (op.Incapacitated) continue;
            activeOperatives.Add(op);
        }
    }

    public void FinishActivation()
    {
        NextPlayer();
        OnActivationDone?.Invoke(this, EventArgs.Empty);
        ResetActiveOperativeList();
        activeOperatives.RemoveAll(op => op.Incapacitated | op.Activated);
        if (activeOperatives.Count == 0)
        {
            NextTurn();
        }
        Debug.Log("Active Operatives Left: " + activeOperatives.Count);

    }


    public void NextTurn()
    {
        TurnNumber++;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
        ResetActiveOperativeList();
    }
    
    private void NextPlayer()
    {

        int index = players.IndexOf(ActivatedPlayer);
        ActivatedPlayer = players[index == players.Count - 1 ? 0 : index + 1];
        Debug.Log("Current Player Activation " + ActivatedPlayer.PlayerName + " id " + index);
    }

}
