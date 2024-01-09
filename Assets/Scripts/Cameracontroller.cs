using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _targetPlayer;
    [SerializeField] private float _lerpRateMoveCamera;

    [SerializeField] private float _offsetY;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetZ;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = _targetPlayer.GetComponent<Rigidbody>();
    }

    private void Update()
    {
       // transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetPlayer.position.x + _offsetX, _targetPlayer.position.y + _offsetY, _targetPlayer.position.z + _offsetZ), _lerpRateMoveCamera * Time.deltaTime) ;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_rigidbody.position.x + _offsetX, _rigidbody.position.y + _offsetY, _rigidbody.position.z + _offsetZ), _lerpRateMoveCamera * Time.deltaTime) ;
        
       transform.LookAt( new Vector3(_target.position.x, _rigidbody.position.y, _target.position.z));      
    }
}
