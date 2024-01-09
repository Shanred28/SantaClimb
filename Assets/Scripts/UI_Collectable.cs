using TMPro;
using UnityEngine;

public class UI_Collectable : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammountText;
    [SerializeField] private Player _player;



    public void ChangeColl()
    { 
    
    
    }

    private void Update()
    {
        _ammountText.text = _player.CollFire.ToString() + "/3";
    }
}
