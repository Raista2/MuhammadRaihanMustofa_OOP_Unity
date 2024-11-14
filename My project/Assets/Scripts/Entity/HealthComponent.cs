using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private float health;

    private void Awake()
    {
        health = maxHealth;
    }

    public float Health => health;

    public void Subtract(float amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} has {health} health left.");
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
