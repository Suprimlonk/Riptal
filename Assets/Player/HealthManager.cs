using UnityEngine;

public class HealthManager : MonoBehaviour{
    public float MAX_HEALTH;
    public float currentHealth;
    public float DamageRecived;
    float FinalDamage;
    void Start()
    {
        currentHealth=MAX_HEALTH;
    }
    void Update()
    {
        RecivedDamage();
        currentHealth -= FinalDamage;
    }
    public void RecivedDamage()
    {
        FinalDamage=DamageRecived;
    }
}