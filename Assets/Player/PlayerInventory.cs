using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance; // Singleton para acceso fácil
    public bool hasKey1 = false;
    public bool hasKey2 = false;
    void Awake(){
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void CollectKey(string keyType){
        if (keyType == "Key1")
            hasKey1 = true;
        else if (keyType == "Key2")
            hasKey2 = true;
        
        Debug.Log("Llave recogida: " + keyType + ". hasKey1: " + hasKey1 + ", hasKey2: " + hasKey2);
    }
    public bool HasAllKeys(){
        return hasKey1 && hasKey2;
    }
}