using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform _trRaycast;
    [SerializeField] private float _jumpSpeed;

    [SerializeField] private Transform _tree;

    [SerializeField] private Transform _aimCharacter;

    [SerializeField] private bool _isJump;
    private float raydistance = 0.03f;
    private Rigidbody _rb;

    [SerializeField] private bool isground;

    [SerializeField] private RotateTree rotateTree;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)== true && isground == true)
        {
            
            Jump();
        }
        IsGrounded();

/*        if (rotateTree.RotateLeft == true)
        {
            Debug.Log("left");
            transform.LookAt(new Vector3(15, transform.position.y, 90));
            
        }

        if (rotateTree.RotateRight == true)
        {
            transform.LookAt(new Vector3(-15, transform.position.y,-180));
        }*/

/*        var dist = _tree.position - transform.TransformPoint(_aimCharacter.position);
        Debug.Log(dist);
        if (dist.x > 3)
        {
            _aimCharacter.position = new Vector3(10, _aimCharacter.transform.position.y, _aimCharacter.transform.position.z);
        }*/

        transform.LookAt(new Vector3( 3,transform.position.y, 90) );

        /*        var angle = Vector3.Angle(transform.forward, _tree.forward);
                if (angle > 90.0f)
                {
                    var adfadf = angle - 90.0f;
                    //transform.rotation = Quaternion.AngleAxis(angle, transform.up);
                    _rb.rotation = Quaternion.AngleAxis(adfadf, transform.up);
                }*/
        //transform.LookAt(new Vector3(30,0 ,0));
        /*        var dirToTarget = _tree.position - transform.position;
                dirToTarget.y = 0;

                var lookAtTargetRot = Quaternion.LookRotation(dirToTarget);

                var moveVector = lookAtTargetRot * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) *
                                 SPEED * Time.deltaTime;*/

    }

    private void IsGrounded()
    {
        if (Physics.Raycast(_trRaycast.position, -Vector3.up, raydistance) == true)
        {
            _isJump = false;
            isground = true;

        }
        else
        {
            _isJump = true;
            isground = false;
        }
           
    }

    private void Jump()
    {
        if (_isJump == true) return;

        
        _rb.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
    }
}

