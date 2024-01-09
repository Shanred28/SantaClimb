using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float _respawnHeight;
    [SerializeField] private EventCollector _eventCollector;

    private CheckPoint _respawnerPoint;

    [SerializeField] private Player _player;

    private void Start()
    {
        _eventCollector.eventTrackPointPassed += OnTrackPointPassed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) == true)
            Respawn();
    }

    private void OnDestroy()
    {
        _eventCollector.eventTrackPointPassed -= OnTrackPointPassed;
    }

    private void OnTrackPointPassed(CheckPoint point)
    {
        _respawnerPoint = point;
    }

    public void Respawn()
    {
        if (_respawnerPoint == null) return;

        // if (_raceStateTracker.State != RaceState.Race) return;

        _player.Respawn(_respawnerPoint.transform.position + _respawnerPoint.transform.up * _respawnHeight, _respawnerPoint.transform.rotation);

        //_carInputControl.Reset();
    }
}
