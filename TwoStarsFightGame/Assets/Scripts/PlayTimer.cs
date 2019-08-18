using System.Collections;
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
        time = 200;
        passtime = 0;
    }

    private void Update()
    {
        passtime += Time.deltaTime;
        time = 200 - passtime;
        if (time <= 0) {
            time = 0;
            GameManager.inst.TimeOver();
        }
        IngameUIManager.inst.UpdatePlaytimeText(time);
    }

}



