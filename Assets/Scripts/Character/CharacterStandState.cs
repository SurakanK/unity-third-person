using System;
using UnityEngine;

public class CharacterStandState : CharacterBaseState
{
    public CharacterStandState(CharacterStateMachine stateMachine) : base(stateMachine) { }


    public override void OnActive()
    {
        stateMachine.velocity.y = Physics.gravity.y;
        stateMachine.inputReader.crouchPerformed += ChangeStateCrouch;
        stateMachine.inputReader.pronePerformed += ChangeStateProne;
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
        var direction = GameUtile.AnimatorDirection(moveDirection);
        
        animator.SetFloat("x", direction.x, 0.1f, Time.deltaTime);
        animator.SetFloat("y", direction.y, 0.1f, Time.deltaTime);
        animator.SetFloat("velocity", moveDirection.magnitude);
        animator.SetBool("Walk", moveDirection.magnitude > 0);
    }

    public override void OnEnded()
    {
        stateMachine.inputReader.crouchPerformed -= ChangeStateCrouch;
        stateMachine.inputReader.pronePerformed -= ChangeStateProne;
    }

    private void ChangeStateCrouch()
    {

    }

    private void ChangeStateProne()
    {

    }
}