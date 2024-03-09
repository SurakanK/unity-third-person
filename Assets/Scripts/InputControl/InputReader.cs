using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputControls.ICharacterActions
{
    private InputControls inputControls;
    public Vector2 moveComposite { get; private set; }
    public Action crouchPerformed { get; private set; }
    public Action pronePerformed { get; private set; }

    private void OnEnable()
    {
        if (inputControls != null)
            return;

        inputControls = new InputControls();
        inputControls.Character.SetCallbacks(this);
        inputControls.Character.Enable();
    }

    public void OnDisable()
    {
        inputControls.Character.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveComposite = context.ReadValue<Vector2>();
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
