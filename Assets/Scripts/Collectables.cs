using UnityEngine;

public class Collectables : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Player player))
        {
            player.AddColl();
            Destroy(gameObject);
        }
    }
}
