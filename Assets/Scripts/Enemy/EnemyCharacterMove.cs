using UnityEngine;

public class EnemyCharacterMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float runSpeed;

    [SerializeField] private float accelerationRate;

    private CharacterController characterController;

    [SerializeField] private float distanceToGround;
    public float DistanceToGround => distanceToGround;
    public bool IsGrounded => characterController.isGrounded || distanceToGround < 0.09f;

    public Vector3 TargetDirectionControl;
    public Vector3 DirectionControl;
    private Vector3 movementDirections;


    public float CurrentSpeed => GetCurrentSpeedByState();
    public bool UpdatePosition;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterController.enabled = true;
    }

    private void Update()
    {
        TargetControlMove();
        UpdateDistanceToGround();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (IsGrounded == true)
        {

            movementDirections = DirectionControl * GetCurrentSpeedByState();

            movementDirections = transform.TransformDirection(movementDirections);
        }

        movementDirections += Physics.gravity * Time.deltaTime;
        if (UpdatePosition == true)
            characterController.Move(movementDirections * Time.deltaTime);
    }

    private void TargetControlMove()
    {
            DirectionControl = Vector3.MoveTowards(DirectionControl, TargetDirectionControl, Time.deltaTime * accelerationRate);

    }

    public float GetCurrentSpeedByState()
    {
        /*        if (isCrouch)
                    return crouchSpeed;*/
/*        if (isAiming)
        {
            if (isSprint == true)
                return sprintSpeed;
            else
                return runSpeed;
        }

        if (isAiming == false)
        {
            if (isSprint == true)
                return sprintSpeed;
            else
                return runSpeed;
        }*/

        return runSpeed;
    }

    private void UpdateDistanceToGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1000) == true)
        {
            distanceToGround = Vector3.Distance(transform.position, hit.point);
        }
    }
}
