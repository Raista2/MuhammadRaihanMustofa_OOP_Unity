using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : EnemyHorizontalMovement
{
    public Bullet bullet;
    public Transform bulletSpawnPoint;
    public float shootInterval = 2f;

    private float shootTimer;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;

    public void Awake()
    {
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );

        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = transform.Find("BulletSpawnPoint");

            if (bulletSpawnPoint == null)
            {
                Debug.LogWarning(this + " tidak menemukan BulletSpawnPoint");
            }
            else
            {
                bulletSpawnPoint.position = new Vector3(0, 1, 0); 
            }
        }
    }

    void Start()
    {
        shootTimer = 0; 
    }

    protected override void Update()
    {
        base.Update();
        
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.SetObjectPool(objectPool);
        int layerIndex = LayerMask.NameToLayer("BulletEnemy");
        newBullet.gameObject.layer = layerIndex;
        return newBullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    
    public Bullet Shoot()
    {
        if (objectPool != null && bulletSpawnPoint != null)
        {
            Bullet bulletInstance = objectPool.Get();
            return bulletInstance;
        }
        return null;
    }
}