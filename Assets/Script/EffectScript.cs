using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public float AppareTime;
    public bool HasSoundEffect;
    public string SoundName = "none";
    void Start()
    {
        Destroy(gameObject, AppareTime);
        if (HasSoundEffect)
        {
            FindObjectOfType<SoundManager>().Play(SoundName);
        }
    }
}
