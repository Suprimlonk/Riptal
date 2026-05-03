using UnityEngine;

public class EnemyHealthManager : MonoBehaviour{
    public float health = 50f;
    public void TakeDamage(float amount){
        health -= amount;
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Salud restante: {health}");
        if (health <= 0){
            Die();
        }
    }
    void Die(){
        Destroy(gameObject);
    }
}