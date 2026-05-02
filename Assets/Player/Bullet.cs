using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f; // Tiempo de vida antes de destruirse

    void Start()
    {
        // Destruir la bala después de su lifetime
        Destroy(gameObject, lifetime);
    }
    void OnCollisionEnter(Collision other) {
        Destroy(gameObject); // Destruir la bala al colisionar con cualquier objeto
        
    }
}