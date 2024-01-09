using UnityEngine;
using UnityEngine.UI;

public class UI_HPVisual : MonoBehaviour
{
    [SerializeField] private Image[] _imageVisualHP;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.ChangeHp.AddListener(SetVisualHP);
        for (int i = 0; i < _player.MaxHitPoints - 1; i++)
        {
            _imageVisualHP[i].gameObject.SetActive(true);
        }
       // SetVisualHP();
    }

    private void OnDestroy()
    {
        _player.ChangeHp.RemoveListener(SetVisualHP);
    }

    private void SetVisualHP()
    {
        var index = _player.CurrentHitPoints -1;

        for (int i = _player.MaxHitPoints - 1; i > index; i--)
        {
            _imageVisualHP[i].gameObject.SetActive(false);
        }
    }
}
