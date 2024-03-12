
using System;
using System.Collections.Generic;
using StatePatternInUnity;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class CharacterStateMachine : StateMachine
{
    public Vector3 velocity;
    public Vector3 direction;
    public float movementSpeed = 5f;
    public List<MoveSpeed> moveSpeeds;
    public float LookRotationDampFactor { get; private set; } = 10f;
    public Transform mainCamera { get; private set; }
    public InputReader inputReader { get; private set; }
    public Animator animator { get; private set; }
    public CharacterController controller { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main.transform;

        inputReader = GetComponent<InputReader>();
        animator = GetComponent<Animator>();
        controller = FindObjectOfType<CharacterController>();

        Initialize(new CharacterStandState(this));
    }

    public float GetMovementSpeed(MoveType moveType, DirectionType directionType)
    {
        var direction = moveSpeeds.Find(e => e.moveType == moveType).directionSpeed;
        return direction.Find(e => e.directionType == directionType).speed;
    }
}