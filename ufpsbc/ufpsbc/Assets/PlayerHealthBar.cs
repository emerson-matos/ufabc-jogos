using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [Tooltip("Image component dispplaying current health")]
    public Image HealthFillImage;

    PlayerHealth m_PlayerHealth;

    public void InitHealthComponent(PlayerHealth playerHealth)
    {
        Debug.Log(playerHealth);
        if (m_PlayerHealth == null)
        {
            m_PlayerHealth = playerHealth;
        }
    }

    void Update()
    {
        // update health bar value
        //Debug.Log(HealthFillImage.fillAmount);
        HealthFillImage.fillAmount = m_PlayerHealth.health.Value / m_PlayerHealth.MaxHealth;
    }
}