using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 20f;
    public int exp = 100;
    public float health = 20f;

    void Start()
    {
        health = maxHealth;    
    }
    
    //When the player/enemy is taking damage, their health decreases by the amount specified on the weapon being used. When the health reaches 0, the enemy simply gets destroyed.
    public void TakingDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        if (health == 0) {
            Destroy(gameObject);
            ScoreScript.scoreValue += exp;
        }
    }

    public void HealingDamage(float healing)
    {
        health = Mathf.Min(health + healing, maxHealth);
       
    }

    //this method increases stats when player levels up
    public void LevelUp()
    {
        maxHealth += 50f;
        health = maxHealth;
    }
    public float FractionOfHealth()
    {
        return health / maxHealth;
    }
}
