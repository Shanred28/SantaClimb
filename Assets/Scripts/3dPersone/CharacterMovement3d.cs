using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement3d : MonoBehaviour
{
    public event UnityAction<Vector3> Land;

    [Header("Movement")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float amimingRanSpeed;
    //[SerializeField] private float crouchSpeed;*/
    [SerializeField] private float _jumpSpeedHight;
    [SerializeField] private float _speedMoveAir;   
    [SerializeField] private float accelerationRate;
    [SerializeField] private float _flyFallDead;

    [Header("State")]
    //[SerializeField] private float crouchHeight;

    private bool isAiming;
    public bool IsAiming => isAiming;
    private bool isJump;
    public bool IsJump => isJump;
    private bool isCrouch;
    public bool IsCrouch => isCrouch;
    private bool isSprint;
    public bool IsSprint => isSprint;
    [SerializeField] private float distanceToGround;
    public float DistanceToGround => distanceToGround;
    public bool IsGrounded => characterController.isGrounded || distanceToGround < 0.09f;

    private float baseCharacterHeight;
    private float baseCharacterOffsetCenter;

    // Controll action animations
    [SerializeField] private EntityActionCollector targetActionCollector;
    private bool isActionAnimation;
    public bool IsActionAnimation => isActionAnimation;


    private CharacterController characterController;
    public Vector3 TargetDirectionControl;
    public Vector3 DirectionControl;
    private Vector3 movementDirections;

    public bool UpdatePosition;
    public float CurrentSpeed => GetCurrentSpeedByState();

    //public EntityContextAction action;
    private Vector3 targetMoveToInteractPoint;
    private bool IsMoveAction = false;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        baseCharacterHeight = characterController.height;
        baseCharacterOffsetCenter = characterController.center.y;
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
            if (isJump == true)
            {
                movementDirections.y = _jumpSpeedHight;
                isJump = false;
            }

            movementDirections = transform.TransformDirection(movementDirections);
        }
        else
        {
            movementDirections = new Vector3(DirectionControl.x * _speedMoveAir, movementDirections.y, DirectionControl.z * _speedMoveAir);
            movementDirections = transform.TransformDirection(movementDirections);
        }

        movementDirections += Physics.gravity * Time.deltaTime;
        if (UpdatePosition == true)
            characterController.Move(movementDirections * Time.deltaTime);

        if (characterController.isGrounded == true && Mathf.Abs(movementDirections.y) > _flyFallDead)
        {
            if (Land != null)
                Land.Invoke(movementDirections);
        }
    }

    private void TargetControlMove()
    {
        if (IsMoveAction == false)
        {
            DirectionControl = Vector3.MoveTowards(DirectionControl, TargetDirectionControl, Time.deltaTime * accelerationRate);
        }
        else
        {
            var dist = Vector3.Distance(transform.position, targetMoveToInteractPoint);
            transform.LookAt(targetMoveToInteractPoint);

            if (dist > 1f)
            {
                DirectionControl = Vector3.MoveTowards(DirectionControl, targetMoveToInteractPoint.normalized, Time.deltaTime * accelerationRate);
            }
            else
                IsMoveAction = false;
        }
    }

    public void Jump()
    {
        if (IsGrounded == false) return;
        if (isAiming == true || isCrouch == true) return;

        isJump = true;
    }

    public void Aiming()
    {
        isAiming = true;
    }
    public void UnAiming()
    {
        isAiming = false;
    }

    public float GetCurrentSpeedByState()
    {
/*        if (isCrouch)
            return crouchSpeed;*/
        if (isAiming)
        {

                return amimingRanSpeed;
        }

        if (isAiming == false)
        {
            if (isSprint == true)
                return sprintSpeed;
            else
                return runSpeed;
        }

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

    public void SetUpdatePosF()
    {
        UpdatePosition = false;
    }

    public void SetUpdatePosT()
    {
        UpdatePosition = true;
    }
}
