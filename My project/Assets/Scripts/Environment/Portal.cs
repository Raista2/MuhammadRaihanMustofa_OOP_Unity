using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    Vector2 newPosition;
    LevelManager levelManager;
    SpriteRenderer spriteRenderer;
    Collider2D collider2D;

    void Start()
    {
        //Mengambil informasi komponen sprite renderer dan collider2D
        //Menoaktifkan sprite renderer dan collider2D
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;

        ChangePosition();
    }

    void ChangePosition()
    {
        //Mengatur posisi baru portal secara random
        newPosition = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));
    }

    void Update()
    {
        //Jika player tidak memiliki weapon, portal akan tetap tidak aktif
        if (GameObject.Find("Player").GetComponentInChildren<Weapon>() != null)
        {
            spriteRenderer.enabled = true;
            collider2D.enabled = true;
        }

        //Menggerakan portal menuju posisi dan rotasi baru
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        transform.eulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);

        //Jika portal sudah berada di posisi baru, maka akan memilih posisi baru
        if (Vector2.Distance(transform.position, newPosition) > 0.5f)
        {
            ChangePosition();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Jika player menyentuh portal, maka akan mengganti scene
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }
}
