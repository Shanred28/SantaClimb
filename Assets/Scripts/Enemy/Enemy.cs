using UnityEngine;

public class Enemy : Destructible
{
    [SerializeField] private GameObject _prefabEffect;

    protected override void OnDeath()
    {
        Instantiate(_prefabEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
