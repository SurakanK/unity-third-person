
using StatePatternInUnity;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class CharacterStateMachine : StateMachine
{
    public Vector3 velocity;
    public Vector3 direction;
    public float movementSpeed = 5f;
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
}