using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public float shootCooldown = 0.5f; // Tiempo entre disparos
    public float shootDamage = 10f; // Daño de cada disparo
    public float shootRange = 100f; // Alcance del disparo
    public GameObject bullet; // Prefab de la bala (para VFX)
    public Transform bulletSpawn; // Punto de origen de la bala
    public float bulletSpeed = 50f; // Velocidad de la bala
    public float bulletLifetime = 2f; // Tiempo de vida de la bala
    private bool canShoot = true;
    void Start()
    {
        // Validaciones básicas
        if (bullet == null) Debug.LogError("Bullet prefab is missing!");
        if (bulletSpawn == null) Debug.LogError("Bullet spawn point is missing!");
    }
    void OnShoot(InputValue value)
    {
        if (value.isPressed && canShoot)
        {
            canShoot = false;
            Invoke(nameof(ResetShoot), shootCooldown);

            // Realizar el disparo con Raycast para daño inmediato
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, shootRange))
            {
                // Aplicar daño si impacta un enemigo
                HealthManager health = hit.collider.GetComponent<HealthManager>();
                if (health != null)
                {
                    health.DamageRecived = shootDamage; // Daño inmediato por Raycast
                }
            }
            // Instanciar bala siempre para VFX
            GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = transform.forward * bulletSpeed;
            }
            Destroy(bulletInstance, bulletLifetime);
        }
    }

    void OnPortalShoot(InputValue value)
    {
        if (value.isPressed)
        {

        }
    }

    void ResetShoot()
    {
        canShoot = true;
    }
}   