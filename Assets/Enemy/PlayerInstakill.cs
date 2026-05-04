using UnityEngine;

public class PlayerInstakill : MonoBehaviour
{
    float damage=100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision){
        HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
        if (playerHealth != null){
            playerHealth.TakeDamage(damage); // Aplica el daño al jugador
        }
    }
}
