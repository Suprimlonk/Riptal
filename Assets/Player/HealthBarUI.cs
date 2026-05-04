using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider;
    public Text healthText;
    public Color fullColor = Color.green;
    public Color lowColor = Color.red;

    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (healthSlider == null)
        {
            Debug.LogWarning("HealthBarUI: healthSlider no está asignado.");
            return;
        }

        float ratio = maxHealth > 0 ? currentHealth / maxHealth : 0f;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        if (healthSlider.fillRect != null)
        {
            Image fillImage = healthSlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
                fillImage.color = Color.Lerp(lowColor, fullColor, ratio);
        }

        if (healthText != null)
            healthText.text = string.Format("Vida: {0}/{1}", Mathf.CeilToInt(currentHealth), Mathf.CeilToInt(maxHealth));
    }
}
