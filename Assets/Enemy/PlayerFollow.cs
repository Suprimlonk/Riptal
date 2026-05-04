using UnityEngine;
public class PlayerFollow : MonoBehaviour
{
    float distance;
    public float DetectionRange = 20f; // Rango de detección del jugador
    public bool PlayerDetected { get; private set; }
    GameObject player; // Referencia al jugador
    void Start()
    {
        player = GameObject.Find("Player"); // Busca el objeto jugador en la escena
    }
    void Update()
    {
        // Validar que el jugador existe antes de cualquier operación
        if (player == null)
        {
            PlayerDetected = false;
            return;
        }
        // Calcula solo la distancia horizontal (ignorando la altura Y)
        Vector3 playerPos = player.transform.position;
        playerPos.y = transform.position.y;
        distance = Vector3.Distance(transform.position, playerPos);
        if (distance < DetectionRange) // Si el jugador está dentro del rango de detección
        {
            PlayerDetected = true;
            Vector3 direction = player.transform.position - transform.position; // Calcula la dirección hacia el jugador
            if (direction.sqrMagnitude < 0.0001f)
                return;
            transform.rotation = Quaternion.LookRotation(direction); // Rota el enemigo para mirar al jugador en 3D
        }
        else
        {
            PlayerDetected = false;
        }
    }
}
