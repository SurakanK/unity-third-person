using StatePatternInUnity;
using UnityEngine;

public class CharacterMoveState : CharacterBaseState
{
    private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private float _animationDampTime = 0.1f;
    private float _crossFadeDuration = 0.1f;

    public CharacterMoveState(CharacterStateMachine stateMachine) : base(stateMachine) { }

    public override void OnActive()
    {
        stateMachine.velocity.y = Physics.gravity.y;
    }

    public override void Update()
    {
        CalculateMoveDirection();
        Move();
    }

    public override void OnEnded()
    {
        base.OnEnded();
    }
}