using UnityEngine;

public class SoundActionCharacter : MonoBehaviour
{
    [SerializeField] private CharacterMovement3d _characterMovement;
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private AudioSource _audioSourceStep;
    [SerializeField] private AudioClip _audioClipStep;

    [SerializeField] private AudioSource _audioSourceJump;

    private Player _player;
    private bool _isDead = false;
    private void Start()
    {
        _player = _characterMovement.transform.GetComponent<Player>();
        _player.EventOnDeath.AddListener(OnDead);
    }

    private void OnDestroy()
    {
        _player.EventOnDeath.RemoveListener(OnDead);
    }

    private void Update()
    {

        if (_characterMovement.IsGrounded == true && _isDead == false)
        {
            Vector3 groundSpeed = _characterController.velocity;
            groundSpeed.y = 0;
            if (groundSpeed.magnitude > 0.03f && _audioSourceStep.isPlaying == false)
            {
                _audioSourceStep.Play();
            }
            //_audioSource.clip = _audioClipStep;
            else if (groundSpeed.magnitude < 0.03f && _audioSourceStep.isPlaying == true)
            {
                _audioSourceStep.Stop();
            }
        }
        else
        {
            _audioSourceStep.Stop();
        }

        if (_characterMovement.IsJump == true)
        {
            _audioSourceJump.Play();
        }
    }

    public void OnDead()
    {
        _isDead = true;
    }
}
