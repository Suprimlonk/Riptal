using UnityEngine;

public class Door : MonoBehaviour
{
    void OnCollisionEnter(Collision collision){
        Debug.Log("Puerta colisionó con: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player")){
            Debug.Log("Player colisionó con puerta");
            if (PlayerInventory.Instance != null && PlayerInventory.Instance.HasAllKeys()){
                // Deshabilitar collider para que el jugador pueda pasar
                Destroy(gameObject);
                Debug.Log("Puerta abierta");
            }
            else{
                Debug.Log("Necesitas ambas llaves para abrir la puerta. Instance: " + (PlayerInventory.Instance != null) + ", HasAllKeys: " + (PlayerInventory.Instance?.HasAllKeys() ?? false));
            }
        }
    }
}