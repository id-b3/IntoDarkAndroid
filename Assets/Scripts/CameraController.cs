using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Configurable Properties")]
    [Tooltip("The default zoom")]
    public float defaultZoom;
    public float zoomMax;
    public float zoomMin;
    public float zoomSpeed;
    //Camera Variables
    private Camera mainCamera;

    //Movement Variables
    private bool touchDown = false;
    [SerializeField]
    private float moveTargetSpeed = 15f;
    [SerializeField]
    private float moveSpeed = 8f;
    private Vector2 moveTarget;
    private Vector2 moveDirection;

    //Zoom Variables
    private float cameraZoom;
    public static CameraController Instance { get; private set; }


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

    void Start()
    {
        //Get the camera
        mainCamera = GetComponentInChildren<Camera>();
        cameraZoom = defaultZoom;
        mainCamera.orthographicSize = cameraZoom;

    }

    public void OnTouchDown(InputAction.CallbackContext context)
    {
        touchDown = context.phase == InputActionPhase.Performed;
    }

    public void OnCameraMove(InputAction.CallbackContext context)
    {
        moveDirection = (touchDown & !OpActionSystem.Instance.OpIsSelected()) ? context.ReadValue<Vector2>() : Vector2.zero;
    }

    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        Debug.Log("Scrolling ");
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        float scrollValue = context.ReadValue<Vector2>().y;
        Zoom(scrollValue);
    }
    public void Zoom(float zoomValue)
    {
        float newZoom = cameraZoom + (zoomValue * zoomSpeed);
        cameraZoom = Mathf.Clamp(newZoom, zoomMin, zoomMax);
    }

    void FixedUpdate()
    {
        moveTarget -= moveTargetSpeed * Time.fixedDeltaTime * moveDirection;
    }

    void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, moveTarget, Time.deltaTime * moveSpeed);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraZoom, Time.deltaTime);
    }

    public Vector3 GetPointerPosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
