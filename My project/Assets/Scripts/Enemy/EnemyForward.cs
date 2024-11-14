using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    float screenHeight;
    float randomX;
    float screenBoundary;

    protected override void Start()
    {
        base.Start();
        
        screenBoundary = Camera.main.orthographicSize * Camera.main.aspect;
        screenHeight = Camera.main.orthographicSize;
        randomX = Random.Range(-screenBoundary, screenBoundary);
        transform.position = new Vector3(randomX, screenHeight + 1f, 0);
    }

    private void Update()
    {
        if (!isActive) return;
        
        transform.Translate(Vector3.down * -moveSpeed * Time.deltaTime);

        if (transform.position.y > screenHeight + 5f|| transform.position.y < -screenHeight - 5f)
        {
            transform.position = new Vector3(randomX, screenHeight + 1f, 0);
        }
    }
}