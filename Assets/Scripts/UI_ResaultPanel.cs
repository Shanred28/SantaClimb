using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UI_ResaultPanel : MonoBehaviour
{
    [SerializeField] private RectTransform _rectPanel;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private GameObject _video;
    [SerializeField] private VideoPlayer _playerVideo;
    [SerializeField] private GameObject _bg_music;

    private void OnDestroy()
    {
        _playerVideo.loopPointReached -= _playerVideo_loopPointReached;
    }

    public void ResaultVictory()
    {
        _bg_music.gameObject.SetActive(false);

        _video.gameObject.SetActive(true);
        _playerVideo.loopPointReached += _playerVideo_loopPointReached;
        
        //_sprite.gameObject.SetActive(true);
        _titleText.text = "Победа";
        
        _playerVideo.loopPointReached += _playerVideo_loopPointReached;
    }

    private void _playerVideo_loopPointReached(VideoPlayer source)
    {
        _video.gameObject.SetActive(false);
        _rectPanel.gameObject.SetActive(true);
        _sprite.gameObject.SetActive(true);
    }

    public void ResaultLose()
    {
        _rectPanel.gameObject.SetActive(true);
        _titleText.text = "Поражение";
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
