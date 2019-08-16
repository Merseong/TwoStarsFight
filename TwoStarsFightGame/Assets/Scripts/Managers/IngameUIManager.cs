using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameUIManager : SingletonBehaviour<IngameUIManager>
{
    public Text playtimeText;
    public GameObject hpPrefab;

    public GameObject[] Player1_hp = new GameObject[100];
    public GameObject[] Player2_hp = new GameObject[100];
    private void Start()
    {
        if (inst != this)
        {
            Destroy(gameObject);
            return;
        }
        else
            SetStatic();
    }


    public void UpdatePlaytimeText(float time)
    {
        string seconds = time.ToString("00");
        playtimeText.text = seconds;
    }

    public void UpdatePlayerHP(int hp, bool playertype)
    {
        if (playertype)
        {
            for(int i = 0; i< 100; i++)
            {
                if( i < 100 - hp)
                    Player1_hp[i].GetComponent<Image>().enabled = false;
            }
                
        }
        else
        {
            for (int i = 0; i < 100; i++)
            {
                if (i >=  hp)
                    Player2_hp[i].GetComponent<Image>().enabled = false;
            }

        }
    }
}
