using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;

public class OpSpawnerButtonUI : MonoBehaviour
{
    private Vector3 startingPos;
    private bool dragging = false;

    public string opName { get; private set; }
    public OpInfo OpStats { get; private set; }

    private void Awake()
    {
        //GetComponentInParent<GridLayoutGroup>().enabled = false;
    }

    private void Start()
    {
        startingPos = transform.position;
        Debug.Log("Local Starting Pos " + startingPos);
    }

    private void LateUpdate()
    {
        if (!dragging & transform.position != startingPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPos, 2);
        }
    }

    public void StartDrag(bool startDrag)
    {
        Debug.Log("Started Dragging " + startDrag);
        dragging = startDrag;
    }

    public void SetOpStats(OpInfo stats)
    {
        Debug.Log("Setting Op Stats");
        OpStats = stats;
    }

}
