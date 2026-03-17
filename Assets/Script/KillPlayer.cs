using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public string nextSceneName;
    public float delay = 0.5f;
    public GameObject fadeout;

    public void Kill()
    {
        if (fadeout != null)
            fadeout.SetActive(true);

        Invoke(nameof(LoadNextScene), delay);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
