using System;
using UnityEngine;

public class CharacterStandState : CharacterBaseState
{
    public CharacterStandState(CharacterStateMachine stateMachine) : base(stateMachine) { }


    public override void OnActive()
    {
        base.OnActive();
        stateMachine.curState = CharacterState.Stand;
        stateMachine.velocity.y = Physics.gravity.y;
        stateMachine.inputReader.crouchPerformed += ChangeStateCrouch;
        stateMachine.inputReader.pronePerformed += ChangeStateProne;
        stateMachine.animator.SetBool("Walk", true);
    }

    public override void Update()
    {
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
        stateMachine.inputReader.crouchPerformed -= ChangeStateCrouch;
        stateMachine.inputReader.pronePerformed -= ChangeStateProne;

        stateMachine.animator.SetBool("Walk", false);
    }

    private void ChangeStateCrouch()
    {
        stateMachine.ChangeState(new CharacterCrouchState(stateMachine));
    }

    private void ChangeStateProne()
    {
        stateMachine.ChangeState(new CharacterProneState(stateMachine));
    }
}