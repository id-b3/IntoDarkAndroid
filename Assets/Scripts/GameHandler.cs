using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Common;
using CW.Common;
using Lean.Touch;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }
    [SerializeField] private LeanDragCamera cameraDrag;
    [SerializeField] private LeanDragLine measuringTape;
    [SerializeField] private Camera mainCamera;

    public bool cameraActive { get; private set; } = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCameraActive();
        if (cameraActive)
        {
            cameraDrag.enabled = true;
        }
        else
        {
            cameraDrag.enabled = false;
        }

    }

    public void CheckCameraActive()
    {
        if (OpActionSystem.Instance.OpIsSelected())
        {
            cameraActive = false;
        }
        else if (ToolSystem.Instance.UsingTool)
        {
            cameraActive = false;
        }
        else if (measuringTape.enabled)
        {
            cameraActive = false;
        }
        else
        {
            cameraActive = true;
        }
    }

    public Vector3 GetWorldPos(Vector3 screenPos)
    {
        return mainCamera.ScreenToWorldPoint(screenPos);
    }
}
