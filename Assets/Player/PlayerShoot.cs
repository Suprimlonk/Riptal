using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float shootCooldown = 0.5f; // Tiempo entre disparos
    float shootTimer = 0f; // Temporizador para controlar el cooldown
    public float shootDamage=10f; // Daño de cada disparo
    public float shootRange=100f; // Alcance del disparo
    public GameObject bullet; // Prefab de la bala
    public Transform bulletSpawn; // Punto de origen de la bala
    public float bulletSpeed = 50f; // Velocidad de la bala
    public float bulletLifetime = 2f; // Tiempo de vida de la bala
    void Shoot(InputValue Value)
    {
        if(value.isPressed){
            if (shootTimer <= 0f)
            {
                // Realizar el disparo
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, shootRange))
                {
                    // Si el disparo impacta algo, aplicar daño si es un enemigo
                    HealthManager health = hit.collider.GetComponent<HealthManager>();
                    if (health != null)
                    {
                        health.DamageRecived = shootDamage; // Aplicar daño al enemigo
                    }
                }
                shootTimer = shootCooldown; // Reiniciar el temporizador de cooldown
            }
        }
    }
    void PortalShoot(InputValue Value)
    {
        if (Value.isPressed)
        {
            
        }
    }
    void Update()
    {
        if (shootTimer > 0f)
            shootTimer -= Time.deltaTime; // Reducir el temporizador con el tiempo
    }  
}   