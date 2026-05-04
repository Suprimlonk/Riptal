using UnityEngine;

public class HealthManager : MonoBehaviour{
    public float MAX_HEALTH = 100f;
    public float currentHealth;
    public HealthBarUI healthBar;

    void Start(){
        currentHealth = MAX_HEALTH;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
        Debug.Log("Jugador recibió " + damage + " de daño. Vida actual: " + currentHealth);
        UpdateHealthUI();
        if (currentHealth <= 0){
            Die();
        }
    }

    public void Heal(float amount){
        currentHealth += amount;
        if (currentHealth > MAX_HEALTH)
            currentHealth = MAX_HEALTH;
        Debug.Log("Jugador curado por " + amount + ". Vida actual: " + currentHealth);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.SetHealth(currentHealth, MAX_HEALTH);
        else
            Debug.LogWarning("HealthManager: healthBar no está asignado.");
    }

    public void Die(){
        if (currentHealth <= 0){
            Vector3 respawnPoint = new Vector3(4, 0, -1);
            transform.position = respawnPoint;
            currentHealth = MAX_HEALTH;
            UpdateHealthUI();
            Debug.Log("Jugador ha muerto. Respawn en: " + respawnPoint);
        }
    }
}