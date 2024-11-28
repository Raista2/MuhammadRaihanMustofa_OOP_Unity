using UnityEngine;

public class EnemyForwardMovement : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] Camera mainCamera;
    Vector3 spawnPosition;

    private void Awake()
    {
        // Menentukan posisi spawn
        if (mainCamera != null)
        {
            float spawnX = Random.Range(0, Screen.width);
            spawnPosition = mainCamera.ScreenToWorldPoint(new Vector3(spawnX, Screen.height, mainCamera.transform.position.z));
            transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        }
        else
        {
            Debug.LogError(this + " tidak menemukan MainCamera");
        }
    }

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.up);
    }

    void OnBecameInvisible()
    {
        transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
    }
}