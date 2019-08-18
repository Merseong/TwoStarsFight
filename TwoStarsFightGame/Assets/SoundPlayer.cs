using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer inst;

    private void Awake()
    {
        if (inst != null) Destroy(gameObject);
        else
        {
            inst = this;
            DontDestroyOnLoad(this);
        }
    }
}
