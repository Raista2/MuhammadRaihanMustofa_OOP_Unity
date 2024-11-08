using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    Vector2 screenBounds;
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    void Start()
    {
        //Mengambil komponen Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        //Perhitungan disesuaikan dengan soal CS
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        //Mengambil input horizontal dan vertical
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        Vector2 newVelocity = rb.velocity;
        Vector2 friction = GetFriction();

        //Perhitungan gerak player horizontal
        if (Mathf.Abs(moveDirection.x) > 0)    //Jika player bergerak
        {
            newVelocity.x += moveVelocity.x * moveDirection.x;
            newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed.x, maxSpeed.x);
        }
        else    //Jika player tidak bergerak horizontal
        {
            newVelocity.x += friction.x * Time.fixedDeltaTime * Mathf.Sign(newVelocity.x);
            if (Mathf.Abs(newVelocity.x) < stopClamp.x)
            {
                newVelocity.x = 0;
            }
        }
        
        //Perhitungan gerak player vertical
        if (Mathf.Abs(moveDirection.y) > 0)
        {
            newVelocity.y += moveVelocity.y * moveDirection.y;
            newVelocity.y = Mathf.Clamp(newVelocity.y, -maxSpeed.y, maxSpeed.y);
        }
        else    //Jika player tidak bergerak vertical
        {
            newVelocity.y += friction.y * Time.fixedDeltaTime * Mathf.Sign(newVelocity.y);
            if (Mathf.Abs(newVelocity.y) < stopClamp.y)
            {
                newVelocity.y = 0;
            }
        }

        rb.velocity = newVelocity;  //Update velocity player
        Debug.Log(rb.velocity);
    }

    public Vector2 GetFriction()
    {
        return new Vector2(
            Mathf.Abs(moveDirection.x) > 0 ? moveFriction.x : stopFriction.x,
            Mathf.Abs(moveDirection.y) > 0 ? moveFriction.y : stopFriction.y
        );
    }

    public void MoveBound()
    {
        //Mengambil ukuran layar
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //Membatasi gerak player agar tidak keluar layar
        Vector2 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + 0.2f, screenBounds.x - 0.2f);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 - 0.1f, screenBounds.y - 0.5f);
        transform.position = viewPos;
    }

    public bool IsMoving()
    {
        return moveDirection.sqrMagnitude > 0;
    }
}
