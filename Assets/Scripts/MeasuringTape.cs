using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasuringTape : MonoBehaviour
{
    private LineRenderer lineRend;
    private Vector2 mousePos;
    private Vector2 startPos;
    private float distance;
    private List<LineRenderer> lineSegments;

    private Vector2 inch, white, blue, red, rest;

    void Awake()
    {
        lineRend = GetComponentInChildren<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.enabled = false;

    }

    void Start()
    {
        lineSegments = new List<LineRenderer>();
        lineSegments.Add(lineRend);
        lineSegments.Add(Instantiate(lineRend, transform.position, Quaternion.identity));
        lineSegments.Add(Instantiate(lineRend, transform.position, Quaternion.identity));
        lineSegments.Add(Instantiate(lineRend, transform.position, Quaternion.identity));
        lineSegments.Add(Instantiate(lineRend, transform.position, Quaternion.identity));
        lineSegments[0].startColor = Color.black;
        lineSegments[0].endColor = Color.black;
        lineSegments[1].startColor = Color.white;
        lineSegments[1].endColor = Color.white;
        lineSegments[2].startColor = Color.blue;
        lineSegments[2].endColor = Color.blue;
        lineSegments[3].startColor = Color.red;
        lineSegments[3].endColor = Color.red;
        lineSegments[4].startColor = Color.gray;
        lineSegments[4].endColor = Color.gray;
    }

    public void RenderTape()
    {

        lineSegments[0].enabled = true;
        mousePos = MouseWorld.GetPosition();

        lineSegments[0].startColor = Color.black;
        distance = (mousePos - startPos).magnitude;

        inch = GetPointAlongLine(Constants.inch);
        white = GetPointAlongLine(Constants.circleM);
        blue = GetPointAlongLine(Constants.squareM);
        red = GetPointAlongLine(Constants.pentagonM);

        lineSegments[0].SetPosition(0, startPos);

        lineSegments[2].SetPosition(0, white);
        lineSegments[3].SetPosition(0, blue);
        lineSegments[4].SetPosition(0, red);

        if (distance < 2.54f)
        {
            lineSegments[0].SetPosition(1, mousePos);
            lineSegments[1].enabled = false;

        }
        else if (distance >= 2.54f && distance < 5.08f)
        {
            lineSegments[1].enabled = true;
            lineSegments[0].SetPosition(1, inch);
            lineSegments[1].SetPosition(0, inch);
            lineSegments[1].SetPosition(1, mousePos);

            lineSegments[2].enabled = false;
            lineSegments[3].enabled = false;
            lineSegments[4].enabled = false;
        }
        else if (distance >= 5.08f && distance < 7.62f)
        {
            lineSegments[2].enabled = true;
            lineSegments[0].SetPosition(1, inch);
            lineSegments[1].SetPosition(0, inch);
            lineSegments[1].SetPosition(1, white);
            lineSegments[2].SetPosition(1, mousePos);

            lineSegments[3].enabled = false;
            lineSegments[4].enabled = false;
        }
        else if (distance >= 7.62f && distance < 15.24f)
        {
            lineSegments[3].enabled = true;
            lineSegments[0].SetPosition(1, inch);
            lineSegments[1].SetPosition(0, inch);
            lineSegments[1].SetPosition(1, white);
            lineSegments[2].SetPosition(1, blue);
            lineSegments[3].SetPosition(1, mousePos);

            lineSegments[4].enabled = false;
        }
        else
        {
            lineSegments[4].enabled = true;
            lineSegments[0].SetPosition(1, inch);
            lineSegments[1].SetPosition(0, inch);
            lineSegments[1].SetPosition(1, white);
            lineSegments[2].SetPosition(1, blue);
            lineSegments[3].SetPosition(1, red);
            lineSegments[4].SetPosition(1, mousePos);
        }
    }

    private Vector2 GetPointAlongLine(float setDist)
    {
        float distRatio = setDist / distance;
        float newX = (1 - distRatio) * startPos.x + distRatio * mousePos.x;
        float newY = (1 - distRatio) * startPos.y + distRatio * mousePos.y;
        return new Vector2(newX, newY);
    }

    public void DisableTape()
    {
        lineSegments[0].enabled = false;
        lineSegments[1].enabled = false;
        lineSegments[2].enabled = false;
        lineSegments[3].enabled = false;
        lineSegments[4].enabled = false;
    }


    public void SetStartPos(Vector3 start)
    {
        startPos = start;
    }

}
