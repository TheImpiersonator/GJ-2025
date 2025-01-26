using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
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
    }
    public void Heal(float healing) {
        //increase health
        currentHealth += healing;
        //clamp to zero
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //debug healing
        Debug.Log(gameObject.name + " healed for " + healing + " points.");
    }
    public void Die() {
        Destroy(this.gameObject);
    }

    private void OnParticleCollision(GameObject other) {
        TakeDamage(1f);
    }
}
