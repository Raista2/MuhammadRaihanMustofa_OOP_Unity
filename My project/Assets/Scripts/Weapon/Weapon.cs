using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;

    private float timer;
    public Transform parentTransform;

    void Awake()
    {
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            collectionCheck,
            defaultCapacity,
            maxSize
        );

        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = transform.Find("BulletSpawnPoint");

            if (bulletSpawnPoint == null)
            {
                Debug.LogWarning("BulletSpawnPoint not found as a child of Weapon.");
            }
            else
            {
                bulletSpawnPoint.position = new Vector3(0,1,0);
            }
        }
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet =  Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation); 
        newBullet.SetObjectPool(objectPool);
        int layerIndex = LayerMask.NameToLayer("BulletPlayer");
        newBullet.gameObject.layer = layerIndex;
        return newBullet;
    }

    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
    }

    private void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0f;
        }
    }

    private Bullet Shoot()
    {
        if (objectPool != null)
        {
            Bullet bulletInstance = objectPool.Get();
            return bulletInstance;
        }
        return null;
    }
}
