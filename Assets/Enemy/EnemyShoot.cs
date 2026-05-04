using UnityEngine;
public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet; // Prefab de la bala
    public Transform bulletSpawn; // Punto de spawn de la bala
    public float BPS = 10f; // Balas por segundo
    PlayerFollow playerFollow; // Referencia al script de seguimiento del jugador
    float nextShootTime;

    void Start(){
        playerFollow = GetComponent<PlayerFollow>();
    }
    void shoot(){
        if (bullet != null && bulletSpawn != null){
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // Crea una instancia de la bala en el punto de spawn
        }
    }
    // Update is called once per frame
    void Update(){
        if (playerFollow != null && playerFollow.PlayerDetected && Time.time >= nextShootTime){ // Si el enemigo está siguiendo al jugador
            shoot(); // Dispara una bala
            nextShootTime = Time.time + 1f / Mathf.Max(BPS, 0.01f);
        }
    }
}
