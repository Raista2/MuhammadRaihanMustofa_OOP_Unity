using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    Player player;

    void Awake()
    {
        player = GetComponentInChildren<Player>();
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(1);
        
        SceneManager.LoadSceneAsync(sceneName);

        //Mengatur posisi player pada scene baru
        player.transform.position = new Vector3(0, -4.5f);
    }

    public void LoadScene(string sceneName)
    {
        //Mengatur animasi transisi scene
        animator.SetTrigger("Start");
        animator.SetTrigger("End");
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
