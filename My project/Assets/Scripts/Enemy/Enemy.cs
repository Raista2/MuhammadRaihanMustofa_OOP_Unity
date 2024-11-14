using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int level = 1;
    [SerializeField] protected float moveSpeed = 5f;
    protected bool isActive = true;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();
        
        rb.gravityScale = 0f;
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
