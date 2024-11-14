using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> pool;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    public void SetObjectPool(IObjectPool<Bullet> bulletPool)
    {
        this.pool = bulletPool;
    }

    private void OnEnable()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        pool?.Release(this);
    }

    void OnBecameInvisible()
    {
        pool?.Release(this);
    }
}
