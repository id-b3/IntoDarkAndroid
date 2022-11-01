using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{

    public static TurnSystem Instance { get; private set; }
    public int turnNumber { get; private set; } = 1;

    public event EventHandler OnTurnChanged;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Turn System! " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void NextTurn()
    {
        turnNumber++;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }
}
