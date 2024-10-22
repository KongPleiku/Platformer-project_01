using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameManeger instance;
    public Vector2 LastCheckPoint;

    [HideInInspector] public bool LastPointChecked = false ;
    LevelLoader Loader;
    private void Awake()
    {
        LastCheckPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        Loader = FindObjectOfType<LevelLoader>();
        HealthSystem player = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        if(player.CurHealth <= 0)
        {
            Loader.ReloadScene();
        }
        if (LastPointChecked)
        {
            Loader.LoadNextLevel();
            LastPointChecked = false;
        }
    }
}
