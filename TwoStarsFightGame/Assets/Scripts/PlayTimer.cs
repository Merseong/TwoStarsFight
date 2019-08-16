﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayTimer : MonoBehaviour
{
    private float time;
    private float passtime;

    private void OnEnable()
    {
        time = 99;
        passtime = 0;
    }

    private void Update()
    {
        passtime += Time.deltaTime;
        time = 100 - passtime;
        IngameUIManager.inst.UpdatePlaytimeText(time);
    }

}


