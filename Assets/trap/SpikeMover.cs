using UnityEngine;

public class SpikeMover : MonoBehaviour
{
    public float upHeight = 1f;
    public float speed = 2f;
    public float waitTime = 1f;

    public AudioSource audioSource;
    public AudioClip spikeUpClip;
    public AudioClip spikeDownClip;

    [HideInInspector]
    public bool isUp;

    private Vector3 startPos;
    private Vector3 upPos;
    private bool goingUp = true;

    void Start()
    {
        startPos = transform.localPosition;
        upPos = startPos + Vector3.up * upHeight;
        StartCoroutine(MoveSpike());
    }

    System.Collections.IEnumerator MoveSpike()
    {
        while (true)
        {
            Vector3 target = goingUp ? upPos : startPos;

            // SET STATUS
            isUp = goingUp;

            // AUDIO DI SINI
            if (goingUp)
                audioSource.PlayOneShot(spikeUpClip);
            else
                audioSource.PlayOneShot(spikeDownClip);

            while (Vector3.Distance(transform.localPosition, target) > 0.01f)
            {
                transform.localPosition = Vector3.MoveTowards(
                    transform.localPosition,
                    target,
                    speed * Time.deltaTime
                );
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
            goingUp = !goingUp;
        }
    }
}


