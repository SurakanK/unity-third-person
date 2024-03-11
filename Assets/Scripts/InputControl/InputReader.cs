using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputControls.ICharacterActions
{
    private InputControls _inputControls;
    public Vector2 moveDirection;
    public Action crouchPerformed;
    public Action pronePerformed;

    private void OnEnable()
    {
        if (_inputControls != null)
            return;

        _inputControls = new InputControls();
        _inputControls.Character.SetCallbacks(this);
        _inputControls.Character.Enable();
    }

    public void OnDisable()
    {
        _inputControls.Character.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        crouchPerformed?.Invoke();
    }

    public void OnProne(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        pronePerformed?.Invoke();
    }
}
