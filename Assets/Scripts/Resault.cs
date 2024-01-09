using UnityEngine;
using UnityEngine.Events;

public class Resault : MonoBehaviour
{
    public UnityEvent _finish;

    [SerializeField] private CheckPointTrack _checkPointTrack;
    [SerializeField] private UI_ResaultPanel _resaultPanel;
    [SerializeField] private Player _player;

    private void Start()
    {
        _checkPointTrack.OnFinishTrack += OnFinishTrack;
       // _player = Player.Instance;
        _player.EventOnDeath.AddListener(GameOver);
    }

    private void OnDestroy()
    {
        _checkPointTrack.OnFinishTrack -= OnFinishTrack;
        _player.EventOnDeath.RemoveListener(GameOver);
    }

    private void OnFinishTrack()
    {
        _finish.Invoke();
        _resaultPanel.gameObject.SetActive(true);
        _resaultPanel.ResaultVictory();
    }

    private void GameOver()
    {
       

            Debug.Log("Dead");
            _resaultPanel.ResaultLose();
    
    }
}
