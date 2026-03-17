using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorWithKeyXR : MonoBehaviour
{
    [Header("Door Settings")]
    public float rotationAngle = 90f;
    public float rotationSpeed = 3.5f;

    [Header("Key Requirement")]
    public GameObject keyInventory; // harus aktif untuk bisa buka pintu

    [Header("Audio")]
    public AudioClip openSound;
    public AudioClip closeSound;

    public bool rotateOnX = false;
    public bool rotateOnY = true;
    public bool rotateOnZ = false;

    private Transform door;
    private AudioSource audioSource;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen;
    private bool isBusy;

    void Start()
    {
        door = transform;
        closedRotation = door.rotation;
        openRotation = GetOpenRotation();

        audioSource = GetComponent<AudioSource>();
        if (!audioSource) audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Dipanggil dari XR Grab Interactable / XR Ray Interactor
    public void TryOpenDoor()
    {
        if (isBusy) return;

        // cek apakah kunci ada di inventory
        if (!keyInventory || !keyInventory.activeInHierarchy)
        {
            Debug.Log("Pintu terkunci, kunci tidak ada!");
            return;
        }

        isOpen = !isOpen;
        PlaySound(isOpen);
        StartCoroutine(RotateDoor(isOpen));
    }

    Quaternion GetOpenRotation()
    {
        Vector3 axis = rotateOnX ? Vector3.right : rotateOnY ? Vector3.up : Vector3.forward;
        return closedRotation * Quaternion.AngleAxis(rotationAngle, axis);
    }

    IEnumerator RotateDoor(bool open)
    {
        isBusy = true;
        Quaternion target = open ? openRotation : closedRotation;

        while (Quaternion.Angle(door.rotation, target) > 0.01f)
        {
            door.rotation = Quaternion.Lerp(door.rotation, target, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        door.rotation = target;
        isBusy = false;
    }

    void PlaySound(bool open)
    {
        if (!audioSource || (!openSound && !closeSound)) return;
        audioSource.clip = open ? openSound : closeSound;
        audioSource.Play();
    }
}
