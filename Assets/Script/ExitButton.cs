using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Aplikasi ditutup");
    }
}