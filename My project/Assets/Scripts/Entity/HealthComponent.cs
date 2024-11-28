using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    private void Awake()
    {
        health = maxHealth;
    }


    public void Subtract(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} has {health} health left.");
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
