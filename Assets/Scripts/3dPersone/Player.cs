using System.Collections;
using UnityEngine;

public class Player : Destructible
{
    public static Player Instance;

    [SerializeField] private CharacterMovement3d _characterMovement3D;
    [SerializeField] private EntityAnimationAction _actionDead;
    [SerializeField] private EntityAnimationAction _getDamage;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
        _characterMovement3D.Land += OnLand;
        _eventOnGetDamage.AddListener(GetDamage);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _eventOnGetDamage.RemoveListener(GetDamage);
    }

    private void OnLand(Vector3 arg0)
    {
        ApplyDamage(1);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        _actionDead.StartAction();
    }

    private void GetDamage()
    {
        _getDamage.StartAction();
    }

    public void Respawn(Vector3 pos, Quaternion rot)
    {
        _characterMovement3D.SetUpdatePosF();
        transform.position = pos;
        transform.rotation = rot;

        StartCoroutine(WaitResp());
    }

    private IEnumerator WaitResp()
    {
        yield return new WaitForSeconds(0.2f);
        Set();
    }

    private void Set()
    {
        _characterMovement3D.SetUpdatePosT();
    }

    private int _collFire;
    public int CollFire => _collFire;
    public void AddColl()
    {
        _collFire++;
    }
}
