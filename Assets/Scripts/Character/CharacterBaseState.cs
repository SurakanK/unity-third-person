using System.Collections;
using System.Collections.Generic;
using StatePatternInUnity;
using UnityEngine;

public abstract class CharacterBaseState : IState
{
    protected readonly CharacterStateMachine stateMachine;

    protected CharacterBaseState(CharacterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void OnInitialized()
    {
        base.OnInitialized();
    }

    public override void OnActive()
    {
        base.OnActive();

        Color color = Color.yellow;
        Logging.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}> Active state: </color> {3}", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), this.GetType().FullName));
    }

    public override void OnEnded()
    {
        base.OnEnded();

        Color color = Color.magenta;
        Logging.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}> Ended state: </color> {3}", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), this.GetType().FullName));
    }

    protected void CalculateMoveDirection() 
    {
        Vector3 cameraForward = new(stateMachine.mainCamera.forward.x, 0, stateMachine.mainCamera.forward.z);
        Vector3 cameraRight = new(stateMachine.mainCamera.right.x, 0, stateMachine.mainCamera.right.z);

        var directionType = GameUtile.DirectionControl(stateMachine.inputReader.moveDirection);
        var direction = GameConfig.AnimatorChrecter[directionType];
        var dirX = direction.X * stateMachine.inputReader.moveDirection.magnitude;
        var dirY = direction.Y * stateMachine.inputReader.moveDirection.magnitude;
        var speed = stateMachine.GetMovementSpeed(stateMachine.curState, directionType);
        stateMachine.direction = cameraForward.normalized * dirY + cameraRight.normalized * dirX;
        stateMachine.velocity.x = stateMachine.direction.x * speed;
        stateMachine.velocity.z = stateMachine.direction.z * speed;
    }

    protected void FaceMoveDirection()
    {
        Vector3 faceDirection = new(stateMachine.velocity.x, 0f, stateMachine.velocity.z);

        if (faceDirection == Vector3.zero)
            return;

        Vector3 cameraForward = new(stateMachine.mainCamera.forward.x, 0, stateMachine.mainCamera.forward.z);
        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, Quaternion.LookRotation(cameraForward), stateMachine.LookRotationDampFactor * Time.deltaTime);
    }

    protected void Move()
    {
        stateMachine.controller.Move(stateMachine.velocity * Time.deltaTime);
    }
}
