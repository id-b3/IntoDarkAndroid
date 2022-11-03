using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeasureAction : BaseAction
{
    private MeasuringTape tape;
    private bool measuring;

    // Start is called before the first frame update
    void Start()
    {
        measuring = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && tape)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                measuring = true;
                tape.SetStartPos(new Vector3(0,0,0)); //InputWorld.tempVector3);
            }
            else if (Input.GetMouseButton(0) && measuring)
            {
                tape.RenderTape();
            }
            else if (Input.GetMouseButtonUp(0) && measuring)
            {
                measuring = false;
                tape.DisableTape();
            }
        }

    }

    public override string GetActionName()
    {
        return "Measure";
    }

    public void SetMeasuringTape(MeasuringTape measuringTape)
    {
        this.tape = measuringTape;
    }

    public override int GetAPCost()
    {
        return 0;
    }
}
