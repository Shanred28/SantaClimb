using UnityEngine;

public class ClickMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;

    [SerializeField] private float _hightJump;


    [SerializeField] private Transform _trRaycast;

    [SerializeField] private bool _isJump;
    private bool _isMove;
    private Vector3 _targetPos;
    private Rigidbody _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isMove == true)
        {
            Movement();

            if (_isJump == true)
            {
                Jump();
                if(IsGrounded() == true)
                    _isJump = false;
            }
            
        }
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) == true)
        {
            SetTargetPosition();
        }
        if (Input.GetMouseButton(1) == true && IsGrounded() == true)
        {
            _isJump = true;
        }
    }

    /*    private void Update()
        {
    *//*        if (Input.GetMouseButton(0) == true)
            {
                SetTargetPosition();
            }*//*



            if (Input.GetMouseButton(1) == true && IsGrounded() == true)
            {
                Jump();
            }

    *//*        if (_isJump == true)
            {
                Jump();
            }*//*

           if (_isMove == true)
            {
                Move();
            }
        }*/

    private void SetTargetPosition()
    {
        _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //_targetPos.z = transform.position.z;

        _isMove = true;
    }


    private void Move()
    {
        Vector3 movement;
        if (_targetPos.z> 0)
        {
            movement = _targetPos;
           // transform.position += transform.forward * _speed * Time.deltaTime;
        }
        else 
        {
             movement = _targetPos;
           // transform.position += -transform.forward * _speed * Time.deltaTime;
        }

        _rb.AddForce(movement * _speed * Time.deltaTime);
        if (transform.position.z == _targetPos.z)
        {
            _isMove = false;
        }
    }
    private float raydistance = 0.3f;
    private bool IsGrounded()
    {
        if (Physics.Raycast(_trRaycast.position, -Vector3.up,  raydistance))
        {

                return true;

        }
        else
            return false;
    }

    private void Movement()
    {
        Vector3 movement = new Vector3(0, 0.0f, _targetPos.z);

        if (_targetPos.z > 0)
        {
            movement = new Vector3(0, transform.position.y, 40);
            transform.LookAt(movement);
            Turned();
        }
        else
        {
            movement = new Vector3(0, transform.position.y, -40);

            transform.LookAt(movement);
        }
        _rb.AddForce(movement * _speed);
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);        
    }

    private void Turned()
    {

    
    }
}
