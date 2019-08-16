using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PlayerNumber {
    player1, player2
}

public class GameManager : SingletonBehaviour<GameManager> {

    public Player[] players = new Player[2];
    public int[] playersLife = new int[2];
    public Transform[] spawnPositions = new Transform[2];

    public Text spawnCountText;
    public Text winnerText;
    public Button restartButton;

    void Awake() {
        SetStatic();
    }

    void Start() {
        //playerInit()
        GameInit();
        StartCoroutine(PlayerRespawn(PlayerNumber.player1));
        StartCoroutine(PlayerRespawn(PlayerNumber.player2));
    }

    private void GameInit() {
        playersLife = new int[] { 3, 3 };

        spawnCountText.text = "";
        winnerText.text = "";
        restartButton.gameObject.SetActive(false);
    }

    public void PlayerDead(PlayerNumber playerNo) {
        if (playersLife[(int)playerNo] > 0) {
            playersLife[(int)playerNo] -= 1;
            StartCoroutine(PlayerRespawn(playerNo));
        } else {
            YoureWinner(playerNo==PlayerNumber.player1 ? PlayerNumber.player2 : PlayerNumber.player1);
        }
    }

    private IEnumerator PlayerRespawn(PlayerNumber playerNo) {
        spawnCountText.text = "";
        for (int i = 0; i < 3; i++) {
            spawnCountText.text = (i + 1).ToString();
            yield return new WaitForSeconds(1);
        }
        spawnCountText.text = "GO!";
        yield return new WaitForSeconds(.5f);
        spawnCountText.text = "";
        Instantiate(players[(int)playerNo], spawnPositions[(int)playerNo].position, Quaternion.Euler(0, 0, 0));
    }

    private void YoureWinner(PlayerNumber playerNo) {
        winnerText.text = "게임종료!\n플레이어" + ((int)playerNo + 1) + "의 승리";
        restartButton.onClick.AddListener(delegate { SceneManager.LoadScene("GameManager Test"); });
        restartButton.gameObject.SetActive(true);
    }
}