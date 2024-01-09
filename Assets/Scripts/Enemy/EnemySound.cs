using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private EnemyCharacterMove _characterMovement;
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private AudioSource _audioSourceStep;

    [SerializeField] private AudioSource _audioSourceAttack;

    private bool _isDead = false;

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
    }

    public void OnDead()
    {
        _isDead = true;
    }
}
