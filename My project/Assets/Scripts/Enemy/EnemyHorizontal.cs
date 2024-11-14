using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private bool movingRight;
    private float screenBoundary;

    protected override void Start()
    {
        base.Start();
        
        screenBoundary = Camera.main.orthographicSize * Camera.main.aspect;
        float spawnX = Random.value > 0.5f ? -screenBoundary : screenBoundary;
        transform.position = new Vector3(spawnX, transform.position.y, 0);
        
        movingRight = spawnX < 0;
    }

    public virtual void Update()
    {
        if (!isActive) return;

        float movement = movingRight ? moveSpeed : -moveSpeed;
        transform.Translate(Vector3.right * movement * Time.deltaTime);

        if (transform.position.x > screenBoundary || transform.position.x < -screenBoundary)
        {
            movingRight = !movingRight;
        }
    }
}