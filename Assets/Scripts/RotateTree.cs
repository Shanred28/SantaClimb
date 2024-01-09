using UnityEngine;

public class RotateTree : MonoBehaviour
{
    [SerializeField] private PointerClickHold _rightButton;
    [SerializeField] private PointerClickHold _leftButton;

    [SerializeField] private float _stepRotate;
    [SerializeField] private float _lerpRateSpeed;

    private bool _isMove;
    private Rigidbody _rb;

    private Quaternion _rotate;
    private float _speedRotate = 10.0f;

    private bool isRotateRight;
    public bool RotateRight => isRotateRight;

    private bool isRotateLeft;
    public bool RotateLeft => isRotateLeft;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rightButton.OnPointerDown += _rightButton_OnPointerDown;
        _rightButton.OnPointerUp += _rightButton_OnPointerUp;

        _leftButton.OnPointerDown += _leftButton_OnPointerDown;
        _leftButton.OnPointerUp += _leftButton_OnPointerUp;

    }

    private void OnDestroy()
    {
        _rightButton.OnPointerDown -= _rightButton_OnPointerDown;
        _rightButton.OnPointerUp -= _rightButton_OnPointerUp;

        _leftButton.OnPointerDown -= _leftButton_OnPointerDown;
        _leftButton.OnPointerUp -= _leftButton_OnPointerUp;
    }
    private void _leftButton_OnPointerUp()
    {
        isRotateLeft = false;
    }

    private void _leftButton_OnPointerDown()
    {
        isRotateLeft = true;
    }

    private void _rightButton_OnPointerUp()
    {
        isRotateRight = false;
    }

    private void _rightButton_OnPointerDown()
    {
        isRotateRight = true;
    }

    private void FixedUpdate()
    {

        var next = Quaternion.RotateTowards(_rb.rotation, _rotate, _lerpRateSpeed * Time.deltaTime);
        _rb.MoveRotation(next);
    }

    private void Update()
    {
        if (isRotateLeft == true && _isMove == false)
        {
            _isMove = true;
            _speedRotate -= _stepRotate;
            _rotate = Quaternion.Euler(0, _speedRotate, 0);
        }

        if (isRotateRight == true && _isMove == false)
        {
            _isMove = true;
            _speedRotate += _stepRotate;
            _rotate = Quaternion.Euler(0, _speedRotate, 0);
        }

        if (transform.rotation == _rotate)
        {
            _isMove = false;
        }
    }
}