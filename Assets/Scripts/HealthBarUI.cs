using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Player player;

    private void Awake()
    {
        player.OnHealthChanged += HandleHealthChanged_OnHealthChanged;
    }
    private void HandleHealthChanged_OnHealthChanged(object sender, float e)
    {
        barImage.fillAmount = player.Health / player.MaxHealth;
    }
}
