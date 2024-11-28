using UnityEngine;

public class EnemyTargetPlayer : Enemy
{
    public float moveSpeed = 2f;

    private Transform player;
    [SerializeField] Camera mainCamera;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        // Menentukan posisi spawn
        if (mainCamera != null)
        {
            float spawnX = Random.Range(0, Screen.width);
            Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(new Vector3(spawnX, Screen.height, mainCamera.transform.position.z));
            transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        }
        else
        {
            Debug.LogError(this + " tidak menemukan MainCamera");
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = moveSpeed * direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}