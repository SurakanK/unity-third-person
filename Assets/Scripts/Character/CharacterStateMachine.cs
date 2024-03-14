
using System;
using System.Collections.Generic;
using Cinemachine;
using FishNet;
using FishNet.Object;
using StatePatternInUnity;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterStateMachine : StateMachine
{
    public Animator animator;

    [Header("Camera")]
    public Transform cameraFollow;
    public Transform cameraLookAt;

    [Header("Character")]
    public Vector3 velocity;
    public Vector3 direction;
    public CharacterState curState;
    public List<MoveSpeed> moveSpeeds;
    public float LookRotationDampFactor { get; private set; } = 10f;
    public Transform mainCamera { get; private set; }
    public CinemachineFreeLook freeLookCamera { get; private set; }
    public InputReader inputReader { get; private set; }
    public CharacterController controller { get; private set; }

    public float GetMovementSpeed(CharacterState moveType, DirectionType directionType)
    {
        var direction = moveSpeeds.Find(e => e.moveType == moveType).directionSpeed;
        return direction.Find(e => e.directionType == directionType).speed;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (base.IsOwner)
        {
            mainCamera = Camera.main.transform;
            inputReader = GetComponent<InputReader>();
            controller = GetComponent<CharacterController>();
            freeLookCamera = FindObjectOfType<CinemachineFreeLook>();

            freeLookCamera.Follow = cameraFollow;
            freeLookCamera.LookAt = cameraLookAt;
        }
        else
        {
            Destroy(GetComponent<InputReader>());
        }

        Initialize(new CharacterStandState(this));
        this.name = $"Client:{Owner.ClientId}";
    }

    [ServerRpc]
    public void SendChangeState(CharacterState state)
    {
        ApplyChangeState(state);
    }

    [ObserversRpc]
    public void ApplyChangeState(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Stand:
                ChangeState(new CharacterStandState(this));
                break;
            case CharacterState.Crouch:
                ChangeState(new CharacterCrouchState(this));
                break;
            case CharacterState.Prone:
                ChangeState(new CharacterProneState(this));
                break;
        }
    }

    [ServerRpc]
    public void SendAnimation(Vector2 moveDirection)
    {
        ApplyAnimation(moveDirection);
    }

    [ObserversRpc]
    public void ApplyAnimation(Vector2 moveDirection)
    {
        var directionType = GameUtile.DirectionControl(moveDirection);
        var direction = GameConfig.AnimatorChrecter[directionType];

        animator.SetFloat("x", direction.X, 0.1f, Time.deltaTime);
        animator.SetFloat("y", direction.Y, 0.1f, Time.deltaTime);
        animator.SetFloat("velocity", moveDirection.magnitude);
    }
}