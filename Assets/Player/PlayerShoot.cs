using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour{
    [Header("Shooting Settings")]
    public float shootCooldown = 0.5f; // Tiempo entre disparos
    public float shootDamage = 10f; // Daño de cada disparo
    public float shootRange = 100f; // Alcance del disparo
    public GameObject bullet; // Prefab de la bala (para VFX)
    public Transform bulletSpawn; // Punto de origen de la bala
    public float bulletSpeed = 50f; // Velocidad de la bala
    public float bulletLifetime = 2f; // Tiempo de vida de la bala
    private bool canShoot = true;
    private bool isShootingPressed = false;
    private Camera cam;
    void Start(){
        // Validaciones básicas
        cam=Camera.main;
        if (bullet == null) Debug.LogError("Bullet prefab is missing!");
        if (bulletSpawn == null) Debug.LogError("Bullet spawn point is missing!");
        if (cam==null)Debug.LogError("Main Camera not found!");
    }
    void Update(){
        // Dispara continuamente mientras se mantiene presionado
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
        // Raycast desde la cámara para respetar rotación vertical
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootRange)){
            HealthManager health = hit.collider.GetComponent<HealthManager>();
            if (health != null){
                health.DamageRecived = shootDamage;
            }
        }
        // Bala orientada hacia donde apunta la cámara
        GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, cam.transform.rotation);
        Rigidbody bulletRb = bulletInstance.GetComponent<Rigidbody>();
        if (bulletRb != null){
            bulletRb.linearVelocity = cam.transform.forward * bulletSpeed;
        }
        Destroy(bulletInstance, bulletLifetime);
    }
    void OnPortalShoot(InputValue button){
        if (button.isPressed){

        }
    }
    void ResetShoot(){
        canShoot = true;
    }
}   