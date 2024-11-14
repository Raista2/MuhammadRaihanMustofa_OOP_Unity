using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    private InvincibilityComponent invincibilityComponent;

    private void Awake()
    {
        if (healthComponent == null)
        {
            healthComponent = GetComponent<HealthComponent>();
        }
        invincibilityComponent = GetComponent<InvincibilityComponent>();
    }

    public void Damage(Bullet bullet)
    {
        if (invincibilityComponent != null && invincibilityComponent.isInvincible)
            return;

        healthComponent.Subtract(bullet.damage);
    }

    public void Damage(int amount)
    {
        if (invincibilityComponent != null && invincibilityComponent.isInvincible)
            return;

        healthComponent.Subtract(amount);
    }
}
