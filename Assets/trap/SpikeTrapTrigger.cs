
using UnityEngine;

public class SpikeTrapTrigger : MonoBehaviour
{
    public int damage = 10;
    bool canDamage = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
           PlayerHealty health = other.GetComponent<PlayerHealty>();

            if (health != null)
            {
                health.TakeDamage(damage);
                canDamage = false;
                Invoke(nameof(ResetDamage), 1f);
            }
        }
    }

    void ResetDamage()
    {
        canDamage = true;
    }
}

