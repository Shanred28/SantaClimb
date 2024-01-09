using System;
using UnityEngine;


[Serializable]
public class CharacterAnimatorParametersNameEnemy
{
    public string NormolizeMovementX;
    public string NormolizeMovementZ;
}

[Serializable]
public class AnimationCrossFadeParametrsEnemy
{
    public string Name;
    public float Duration;
}

public class EnemyCharacterAnimationState : MonoBehaviour
{ 
    private const float INPUT_CONTROL_LERP = 10f;
    [SerializeField] private CharacterController targetCharacterController;
    [SerializeField] private EnemyCharacterMove characterMovement;

    [SerializeField]
    [Space(5)]
    private CharacterAnimatorParametersNameEnemy animatorParametersName;

    [SerializeField] private Animator targetAnimator;

    [SerializeField]
    [Header("Fades")]
    [Space(5)]
    private AnimationCrossFadeParametrsEnemy fallFade;

    private Vector3 inputControl;

    private void LateUpdate()
    {
       // Vector3 movementSpeed = transform.InverseTransformDirection(targetCharacterController.velocity);
        inputControl = Vector3.MoveTowards(inputControl, characterMovement.DirectionControl, Time.deltaTime * INPUT_CONTROL_LERP);
        targetAnimator.SetFloat(animatorParametersName.NormolizeMovementX, inputControl.x);
        targetAnimator.SetFloat(animatorParametersName.NormolizeMovementZ, inputControl.z);

    }
}
