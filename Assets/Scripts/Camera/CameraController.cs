using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraController : MonoBehaviour, InputControls.ICameraActions
{
    [SerializeField] private Vector2 _speedRotation;

    private CinemachineFreeLook _freeLookCamera;
    private InputControls _inputControls;
    private bool _isTouchOnArea;

    private readonly string targetTag = "TouchArea";

    private void OnEnable()
    {
        _freeLookCamera = GetComponent<CinemachineFreeLook>();

        if (_inputControls != null)
            return;

        _inputControls = new InputControls();
        _inputControls.Camera.SetCallbacks(this);
        _inputControls.Camera.Enable();
    }

    public void OnDisable()
    {
        _inputControls.Camera.Disable();
    }

    public void OnTouchStart(InputAction.CallbackContext context)
    {
        var touchStart = context.ReadValue<Vector2>();

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            _isTouchOnArea = IsPointerOnArea(touchStart);
        }
    }

    public void OnTouchMove(InputAction.CallbackContext context)
    {
        if (!_isTouchOnArea) return;

        var delta = context.ReadValue<Vector2>();
        _freeLookCamera.m_XAxis.Value += delta.x * _speedRotation.x;
        _freeLookCamera.m_YAxis.Value += delta.y * _speedRotation.y *-1;
    }

    private bool IsPointerOnArea(Vector2 screenPosition)
    {
        // Check if the touch is over a UI object
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;

        // Raycast using the event system to determine if the touch is over UI
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag(targetTag))
            {
                return true;
            }
        }

        return false;
    }
}