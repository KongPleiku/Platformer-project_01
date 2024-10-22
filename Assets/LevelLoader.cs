using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public float TranTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void ReloadScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    public void ReStartLevel()
    {
        GameManeger GM = FindObjectOfType<GameManeger>();
        GameObject startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        GM.LastCheckPoint = startPoint.gameObject.transform.position;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    IEnumerator LoadLevel(int LevelInDex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(TranTime);
        SceneManager.LoadScene(LevelInDex);
    }
}
