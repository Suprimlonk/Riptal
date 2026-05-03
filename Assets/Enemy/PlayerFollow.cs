using UnityEngine;
public class PlayerFollow : MonoBehaviour
{
    GameObject player; // Referencia al jugador
    void Start()
    {
        player = GameObject.Find("Player"); // Busca el objeto jugador en la escena
    }
    void Update()
    {
        if (player == null)
            return;
        Vector3 direction = player.transform.position - transform.position; // Calcula la dirección hacia el jugador
        if (direction.sqrMagnitude < 0.0001f)
            return;
        transform.rotation = Quaternion.LookRotation(direction); // Rota el enemigo para mirar al jugador en 3D
    }
}
