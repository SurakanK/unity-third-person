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
        stateMachine.animator.SetBool("Walk", true);

        if (stateMachine.IsOwner)
        {
            stateMachine.inputReader.crouchPerformed += ChangeStateCrouch;
            stateMachine.inputReader.pronePerformed += ChangeStateProne;
        }
    }

    public override void Update()
    {
        if (stateMachine.IsOwner)
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
            stateMachine.inputReader.crouchPerformed -= ChangeStateCrouch;
            stateMachine.inputReader.pronePerformed -= ChangeStateProne;
        }

        stateMachine.animator.SetBool("Walk", false);
    }

    private void ChangeStateCrouch()
    {
        stateMachine.SendChangeState(CharacterState.Crouch);
    }

    private void ChangeStateProne()
    {
        stateMachine.SendChangeState(CharacterState.Prone);
    }
}