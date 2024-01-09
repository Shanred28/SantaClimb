using UnityEngine;

public class DamageAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Player player))
        {
            player.ApplyDamage(1);
        }
    }
}
