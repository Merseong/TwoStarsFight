using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Player[] players;
    public Transform[] spawnPositions;
    public Text spawnCountText;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //playerInit()
        GameInit();
        PlayerRespawn(0);
        PlayerRespawn(1);
    }

    private void GameInit()
    {
        spawnCountText.text = "";
    }

    public void PlayerRespawn(int playerNo)
    {
        spawnCountText.text = "";
        StartCoroutine(PlayerRespawnCoroutine(playerNo));
    }

    IEnumerator PlayerRespawnCoroutine(int playerNo)
    {
        for (int i = 0; i < 3; i++)
        {
            spawnCountText.text = (i + 1).ToString();
            yield return new WaitForSeconds(1f);
        }
        spawnCountText.text = "";
        Instantiate(players[playerNo], spawnPositions[playerNo].position, Quaternion.Euler(0,0,0));
    }
}