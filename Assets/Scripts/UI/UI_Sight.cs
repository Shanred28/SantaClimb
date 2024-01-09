using UnityEngine;
using UnityEngine.UI;

public class UI_Sight : MonoBehaviour
{
    [SerializeField] private CharacterMovement3d characterMovement;
    [SerializeField] private Image imageSight;

    private void Update()
    {
        imageSight.enabled = characterMovement.IsAiming;
    }
}
