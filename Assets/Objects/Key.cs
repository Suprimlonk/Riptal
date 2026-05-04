using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyType = "Key1"; // Cambia a "Key2" para la segunda llave
    public float rotationSpeed = 50f;
    public float floatSpeed = 2f;
    public float floatHeight = 0.5f;
    
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        // Rotar la llave
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        
        // Mover arriba y abajo
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name + " Tag: " + collision.gameObject.tag);
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión con Player");
            if (PlayerInventory.Instance != null)
            {
                PlayerInventory.Instance.CollectKey(keyType);
                gameObject.SetActive(false); // Ocultar la llave
                Debug.Log("Llave " + keyType + " recogida y ocultada");
            }
            else
            {
                Debug.LogError("PlayerInventory.Instance es null. Asegúrate de que PlayerInventory esté en el Player.");
            }
        }
        else
        {
            Debug.Log("Colisión no es con Player");
        }
    }
}
