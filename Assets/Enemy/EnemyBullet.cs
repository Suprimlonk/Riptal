using UnityEngine;
public class EnemyBullet : MonoBehaviour
{
    [HideInInspector]
    public float damage = 10f; // Daño que inflige la bala
    public float bulletSpeed = 20f; // Velocidad de la bala
    public float lifetime = 5f; // Tiempo de vida de la bala antes de destruirse
    public float BPS = 10f; // Balas por segundo
    Rigidbody rb; // Componente Rigidbody
    
    void Start(){
        rb = GetComponent<Rigidbody>();
        if (rb != null){
            rb.linearVelocity = transform.forward * bulletSpeed; // Dispara la bala hacia adelante
        }
        Destroy(gameObject, lifetime); // Destruye la bala después de su tiempo de vida
    }
    
    void OnCollisionEnter(Collision collision){
        HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
        if (playerHealth != null){
            playerHealth.TakeDamage(damage); // Aplica el daño al jugador
            Debug.Log("Bala del enemigo impactó al jugador. Vida restante: " + playerHealth.currentHealth);
        }
        Destroy(gameObject); // Destruye la bala al colisionar
    }
}