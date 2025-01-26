using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void DeathEventCall();
    public DeathEventCall OnDeath;
    public delegate void DamagedEventCall();
    public DamagedEventCall OnDamaged;
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
            Die();
        }
        OnDamaged?.Invoke();
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
        OnDeath?.Invoke();
        Destroy(this.gameObject);
    }

    private void OnParticleCollision(GameObject other) {
        TakeDamage(1f);
    }
}
