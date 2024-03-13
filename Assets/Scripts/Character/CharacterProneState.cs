using System;
using System.Collections;
using UnityEngine;

public class CharacterProneState : CharacterBaseState
{
    public CharacterProneState(CharacterStateMachine stateMachine) : base(stateMachine) { }

    private bool animFinished = false;

    public override void OnActive()
    {
        base.OnActive();
        animFinished = false;
        stateMachine.curState = CharacterState.Prone;
        stateMachine.velocity.y = Physics.gravity.y;
        stateMachine.inputReader.crouchPerformed += ChangeStateCrouch;
        stateMachine.inputReader.pronePerformed += ChangeStateStand;

        stateMachine.StartCoroutine(StartCrouch());
    }

    public override void Update()
    {
        if (!animFinished) return;

        CalculateMoveDirection();
        FaceMoveDirection();
        Move();
        Animation();
    }


    private void Animation()
    {
        var animator = stateMachine.animator;
        var moveDirection = stateMachine.inputReader.moveDirection;
        var directionType = GameUtile.DirectionControl(moveDirection);
        var direction = GameConfig.AnimatorChrecter[directionType];

        animator.SetFloat("x", direction.X, 0.1f, Time.deltaTime);
        animator.SetFloat("y", direction.Y, 0.1f, Time.deltaTime);
        animator.SetFloat("velocity", moveDirection.magnitude);
    }

    public override void OnEnded()
    {
        base.OnEnded();
        stateMachine.inputReader.crouchPerformed -= ChangeStateStand;
        stateMachine.inputReader.pronePerformed -= ChangeStateCrouch;
        stateMachine.StopAllCoroutines();
        stateMachine.animator.SetBool("Prone", false);
    }

    private void ChangeStateStand()
    {
        if (!animFinished) return;
        stateMachine.StartCoroutine(ChangeToStateStand());
    }

    private void ChangeStateCrouch()
    {
        if (!animFinished) return;
        stateMachine.StartCoroutine(ChangeToStateCrouch());
    }

    private IEnumerator StartCrouch()
    {
        stateMachine.animator.SetBool("Prone", true);
        yield return new WaitForSeconds(1.63f);
        animFinished = true;
    }

    private IEnumerator ChangeToStateStand()
    {
        animFinished = false;
        stateMachine.animator.SetBool("Prone", false);
        stateMachine.animator.SetBool("Walk", true);
        yield return new WaitForSeconds(1.4f);
        stateMachine.ChangeState(new CharacterStandState(stateMachine));
    }

    private IEnumerator ChangeToStateCrouch()
    {
        animFinished = false;
        stateMachine.animator.SetBool("Prone", false);
        stateMachine.animator.SetBool("Crouch", true);
        yield return new WaitForSeconds(0.5f);
        stateMachine.ChangeState(new CharacterCrouchState(stateMachine));
    }

}