using System;
using System.Collections;
using UnityEngine;

public class CharacterCrouchState : CharacterBaseState
{
    public CharacterCrouchState(CharacterStateMachine stateMachine) : base(stateMachine) { }

    private bool animFinished = false;

    public override void OnActive()
    {
        base.OnActive();
        animFinished = false;
        stateMachine.curState = CharacterState.Crouch;
        stateMachine.velocity.y = Physics.gravity.y;
        stateMachine.inputReader.crouchPerformed += ChangeStateStand;
        stateMachine.inputReader.pronePerformed += ChangeStateProne;

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
        stateMachine.inputReader.pronePerformed -= ChangeStateProne;
        stateMachine.StopAllCoroutines();
        stateMachine.animator.SetBool("Crouch", false);
    }

    private void ChangeStateStand()
    {
        if (!animFinished) return;
        stateMachine.StartCoroutine(ChangeToStateStand());
    }

    private void ChangeStateProne()
    {
        if (!animFinished) return;
        stateMachine.StartCoroutine(ChangeToStateProne());
    }

    private IEnumerator StartCrouch()
    {
        stateMachine.animator.SetBool("Crouch", true);
        yield return new WaitForSeconds(0.875f);
        animFinished = true;
    }

    private IEnumerator ChangeToStateStand()
    {
        animFinished = false;
        stateMachine.animator.SetBool("Crouch", false);
        stateMachine.animator.SetBool("Walk", true);
        yield return new WaitForSeconds(0.7f);
        stateMachine.ChangeState(new CharacterStandState(stateMachine));
    }

    private IEnumerator ChangeToStateProne()
    {
        animFinished = false;
        stateMachine.animator.SetBool("Crouch", false);
        stateMachine.animator.SetBool("Prone", true);
        yield return new WaitForSeconds(0.3f);
        stateMachine.ChangeState(new CharacterProneState(stateMachine));
    }
}