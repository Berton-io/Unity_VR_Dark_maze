using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarIM;
    public PlayerHealth player;

    void Start()
    {
        if (player == null)
            player = Object.FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        healthBarIM.fillAmount = player.currentHealth / player.maxHealth;
    }
}

