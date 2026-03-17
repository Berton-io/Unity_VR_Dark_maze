using UnityEngine;

public class PickupKey : MonoBehaviour
{
    public GameObject keyInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<KeyInventory>().hasKey = true;

            if (keyInventory != null)
                keyInventory.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}

