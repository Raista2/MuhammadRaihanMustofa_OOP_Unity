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

        var hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            if (this.gameObject.tag == "Bullet")
                hitbox.Damage(bullet);
            else 
                hitbox.Damage(damage);
            

            var invincibility = other.GetComponent<InvincibilityComponent>();
            if (invincibility != null)
            {
                invincibility.Flash();
            }
        }
    }
}