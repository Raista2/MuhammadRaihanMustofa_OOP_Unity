using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Player Instance;
    PlayerMovement playerMovement;
    Animator animator;

    void Awake()
    {
        //Implementasi singleton pattern
        //Agar hanya ada satu instance player
        if (Instance == null)
        {
            Instance  = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        //Mengambil komponen PlayerMovement dan Animator
        playerMovement = GetComponent<PlayerMovement>();
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Update movement player
        playerMovement.Move();
    }

    void LateUpdate()
    {
        //Update animasi player
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
