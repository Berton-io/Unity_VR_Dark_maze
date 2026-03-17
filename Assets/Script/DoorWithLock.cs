using UnityEngine;

public class DoorsWithLock : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    public GameObject keyInventory;

    public AudioSource doorSound;
    public AudioSource lockedSound;

    private bool inReach;
    private bool isOpen;

    void Start()
    {
        inReach = false;
        isOpen = false;
        openText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            openText.SetActive(false);
        }
    }

    void Update()
    {
        if (!inReach) return;

        if (Input.GetButtonDown("Interact"))
        {
            if (keyInventory.activeInHierarchy)
            {
                ToggleDoor();
            }
            else
            {
                lockedSound.Play();
            }
        }
    }

    void ToggleDoor()
    {
        isOpen = !isOpen;

        door.SetBool("Open", isOpen);
        door.SetBool("Closed", !isOpen);

        doorSound.Play();
    }
}
