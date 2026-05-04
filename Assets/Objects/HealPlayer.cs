using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public float HealNUM=50;
    GameObject player;
    HealthManager healthManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player"); // Referencia a player
        if (player != null)
            healthManager = player.GetComponent<HealthManager>();
    }
    // Update is called once per frame
    void Update(){
    }
    private void OnCollisionEnter(Collision collision) {
        if (healthManager != null){
            healthManager.Heal(HealNUM);
            Destroy(gameObject);
        }
    }
}
