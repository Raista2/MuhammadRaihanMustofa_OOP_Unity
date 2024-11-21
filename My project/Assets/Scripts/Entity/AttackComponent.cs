using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage = 10;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == gameObject.tag) //Collision dengan tag sama
            return;

        if (other.GetComponent<HitboxComponent>() != null)
        {
            HitboxComponent hitbox = other.GetComponent<HitboxComponent>();

            if (bullet != null)
            {
                hitbox.Damage(bullet.damage);
            }

            hitbox.Damage(damage);
        }

        if (other.GetComponent<InvincibilityComponent>() != null)
        {
            other.GetComponent<InvincibilityComponent>().Flash();
        }
    }
}