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

        if (stateMachine.IsOwner)
        {
            stateMachine.inputReader.crouchPerformed += ChangeStateCrouch;
            stateMachine.inputReader.pronePerformed += ChangeStateStand;
        }

        stateMachine.StartCoroutine(StartCrouch());
    }

    public override void Update()
    {
        if (stateMachine.IsOwner && animFinished)
        {
            CalculateMoveDirection();
            FaceMoveDirection();
            Move();
            stateMachine.SendAnimation(stateMachine.inputReader.moveDirection);
        }
    }

    public override void OnEnded()
    {
        base.OnEnded();

        if (stateMachine.IsOwner)
        {
            stateMachine.inputReader.crouchPerformed -= ChangeStateStand;
            stateMachine.inputReader.pronePerformed -= ChangeStateCrouch;
        }

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
        stateMachine.SendChangeState(CharacterState.Stand);
    }

    private IEnumerator ChangeToStateCrouch()
    {
        animFinished = false;
        stateMachine.animator.SetBool("Prone", false);
        stateMachine.animator.SetBool("Crouch", true);
        yield return new WaitForSeconds(0.5f);
        stateMachine.SendChangeState(CharacterState.Crouch);
    }

}