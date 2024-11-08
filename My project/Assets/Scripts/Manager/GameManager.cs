using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        //Implementasi singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LevelManager = GetComponentInChildren<LevelManager>();

        //Mengatur agar GameManager dan camera tidak dihancurkan ketika berpindah scene
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }
}
