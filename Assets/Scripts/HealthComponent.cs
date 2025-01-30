using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    /*Death Delegate*/
    public delegate void DeathEventCall();
    public DeathEventCall OnDeath;
    /*Damaged Delegate*/
    public delegate void DamagedEventCall();
    public DamagedEventCall OnDamaged;
    /*Heal Delegate*/
    public delegate void HealedEventCall();
    public HealedEventCall OnHeal;

    public float maxHealth;
    public float currentHealth;

    void Start() {
        //set initial health to max
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage) {

        //reduce health by amount of damage
        currentHealth -= damage;
        //clamp to zero
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //if health is zero, then die
        if (currentHealth <= 0) {
            Die(); //Call die method
        }
        OnDamaged?.Invoke(); //INVOKE DAMAGE EVENT
    }
    public void Heal(float healing) {
        //increase health
        currentHealth += healing;
        //clamp to zero
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //debug healing
        Debug.Log(gameObject.name + " healed for " + healing + " points.");
        OnHeal?.Invoke();
    }
    public void Die() {
        OnDeath?.Invoke(); //INVOKES DEATH EVENT
    }

    public float get_HealthPercent() { 
        return  currentHealth/maxHealth;
    }
    private void OnParticleCollision(GameObject other) {
        TakeDamage(1f);
    }
}
