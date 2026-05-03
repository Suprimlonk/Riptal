using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage = 0f; // Daño que inflige la bala, asignado por PlayerShoot

    void OnCollisionEnter(Collision other)
    {
        // Solo efecto visual al colisionar, el daño ya lo hizo el Raycast
        Destroy(gameObject);
    }
}