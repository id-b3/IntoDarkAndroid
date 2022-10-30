using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{

    public static Vector3 GetPosition(){
        Vector3 mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mouseCoords;
    }
}
