using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Daño que aplica la bala
    public float lifetime = 2f; // Tiempo de vida antes de destruirse

    void Start()
    {
        // Destruir la bala después de su lifetime
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Aplicar daño si colisiona con un enemigo
        HealthManager health = collision.gameObject.GetComponent<HealthManager>();
        if (health != null)
        {
            health.TakeDamage(damage); // Asume que HealthManager tiene un método TakeDamage
        }

        // Destruir la bala al impactar
        Destroy(gameObject);
    }
}