using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public float shootCooldown = 0.5f;
    public float shootDamage = 10f;
    public float shootRange = 100f;
    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed = 50f;
    public float bulletLifetime = 2f;
    private bool canShoot = true;
    private bool isShootingPressed = false;
    private Camera cam;
    void Start(){
        cam = Camera.main;
        if (bullet == null) Debug.LogError("Bullet prefab is missing!");
        if (bulletSpawn == null) Debug.LogError("Bullet spawn point is missing!");
        if (cam == null) Debug.LogError("Main Camera not found!");
    }
    void Update(){
        if (isShootingPressed && canShoot){
            PerformShot();
            canShoot = false;
            Invoke(nameof(ResetShoot), shootCooldown);
        }
    }
    void OnShoot(InputValue button){
        isShootingPressed = button.isPressed;
    }
    void PerformShot(){
        // Debug visual para ver el rayo en Scene view
        Debug.DrawRay(cam.transform.position, cam.transform.forward * shootRange, Color.red, 1f);

        RaycastHit hit;
        // Usa shootMask para ignorar el collider del propio jugador
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootRange, ~LayerMask.GetMask("Player"))){
            Debug.Log($"Raycast golpeó: {hit.collider.gameObject.name} con tag: {hit.collider.tag}");
            EnemyHealthManager enemyHealth = hit.collider.GetComponent<EnemyHealthManager>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(shootDamage);
                Debug.Log($"Daño aplicado: {shootDamage}");
            }
        }
        else{
            Debug.Log("Raycast no golpeó nada");
        }
        // Bala visual
        GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, cam.transform.rotation);
        Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
        if (bulletRb != null){
            bulletRb.linearVelocity = cam.transform.forward * bulletSpeed;
        }
        Destroy(bulletInstance, bulletLifetime);
    }
    void OnPortalShoot(InputValue button) { }
    void ResetShoot() => canShoot = true;
}