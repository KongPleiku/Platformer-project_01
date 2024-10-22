using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    GameManeger GM;
    void Start()
    {
         GM = FindObjectOfType<GameManeger>();
        transform.position = GM.LastCheckPoint;
    }
}
