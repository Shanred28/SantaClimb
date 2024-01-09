using UnityEngine;
using UnityEngine.Events;

public class EventCollector : MonoBehaviour
{
    public event UnityAction<CheckPoint> eventTrackPointPassed;

    [SerializeField] private CheckPointTrack checkPointTrack;
    [SerializeField] private CharacterMovement3d _characterMovement3D;

    [SerializeField] private PlayerRespawn _playerRespawn;
    [SerializeField] private Player _player;

    private void Awake()
    {
        checkPointTrack.TrackPointTriggered += OnTrackPointTriggered;
        _characterMovement3D.Land += OnFallingLand;
    }



    private void OnDestroy()
    {
        checkPointTrack.TrackPointTriggered -= OnTrackPointTriggered;
        _characterMovement3D.Land -= OnFallingLand;
    }
    private void OnFallingLand(Vector3 arg0)
    {
        //TODO
        if (_player.CurrentHitPoints - 1 != 0)
        {
            _playerRespawn.Respawn();
        }       
    }

    private void OnTrackPointTriggered(CheckPoint trackPoint)
    {
        eventTrackPointPassed?.Invoke(trackPoint);
    }
}
