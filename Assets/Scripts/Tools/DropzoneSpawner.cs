using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class DropzoneSpawner : MonoBehaviour
{
    [SerializeField] private Operative templateOp;
    [SerializeField] private LayerMask dropzoneLayer;
    private OpInfo opInfo;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void InDropzone()
    {
        Vector3 worldCoords = Camera.main.ScreenToWorldPoint(transform.position);
        Debug.Log("Dropped in Dropzone" + worldCoords);
        //Vector3 fingerPos = Camera.main.ScreenToWorldPoint(finger.LastScreenPosition);
        Collider2D overDropzone = Physics2D.OverlapPoint(worldCoords, dropzoneLayer);
        if (overDropzone)
        {
            OpSpawnerButtonUI opButt = GetComponentInParent<OpSpawnerButtonUI>();
            Debug.Log("Collided over Dropzone" + opButt);
            opInfo = opButt.OpStats;
            Operative op = Instantiate(templateOp, new Vector3(worldCoords.x, worldCoords.y, 0), Quaternion.identity);
            op.InitOp(opInfo.OpName, opInfo.M, opInfo.APL, opInfo.GA, opInfo.DF, opInfo.S, opInfo.W, opInfo.B, opInfo.opWeaponList);
            op.SetOwnerPlayer(TurnSystem.Instance.ActivatedPlayer.PlayerIndex);
            Debug.Log("Over Dropzone" + gameObject.name + " owner " + TurnSystem.Instance.ActivatedPlayer.PlayerName);
            Destroy(gameObject);
        }
    }
}
