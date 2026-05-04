using System.Collections;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour{
    public float health = 50f;
    public Color hitColor = Color.white;
    public float hitFlashDuration = 0.2f;

    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    public void TakeDamage(float amount){
        health -= amount;
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Salud restante: {health}");
        if (rend != null)
            StartCoroutine(FlashHit());
        if (health <= 0){
            Die();
        }
    }

    IEnumerator FlashHit()
    {
        rend.material.color = hitColor;
        yield return new WaitForSeconds(hitFlashDuration);
        rend.material.color = originalColor;
    }

    void Die(){
        Destroy(gameObject);
    }
}
