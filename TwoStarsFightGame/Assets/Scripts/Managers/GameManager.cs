using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Spine.Unity;

public enum PlayerNumber {
    player1, player2
}

public class GameManager : SingletonBehaviour<GameManager> {

    public GameObject[] playersInst = new GameObject[6];
    public GameObject[,] players = new GameObject[2,3];
    public int[] playersLife = new int[2];
    public Transform[] spawnPositions = new Transform[2];

    public Player[] currentPlayer = new Player[2];

    public Text spawnCountText;
    public Text winnerText;
    public Button restartButton;

    void Awake() {
        SetStatic();
    }

    void Start() {
        GameInit();
        StartCoroutine(PlayerRespawn(PlayerNumber.player1));
        StartCoroutine(PlayerRespawn(PlayerNumber.player2));
    }

    private void GameInit() {
        restartButton.onClick.RemoveAllListeners();

        playersLife = new int[] { 3, 3 };
        players[0, 0] = playersInst[0];
        players[0, 1] = playersInst[1];
        players[0, 2] = playersInst[2];
        players[1, 0] = playersInst[3];
        players[1, 1] = playersInst[4];
        players[1, 2] = playersInst[5];

        foreach (var player in players)
        {
            player.SetActive(false);
        }

        spawnCountText.text = "";
        winnerText.text = "";
        restartButton.gameObject.SetActive(false);
    }

    public void PlayerDead(PlayerNumber playerNo) {
        Destroy(currentPlayer[(int)playerNo].gameObject);
        if (playersLife[(int)playerNo] > 1) {
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
        currentPlayer[(int)playerNo] = players[(int)playerNo, playersLife[(int)playerNo] - 1].GetComponent<Player>();
        players[(int)playerNo, playersLife[(int)playerNo] - 1].SetActive(true);
        players[(int)playerNo, playersLife[(int)playerNo] - 1].transform.position = spawnPositions[(int)playerNo].position;

        IngameUIManager.inst.ResetHPBar(playerNo);
        yield return null;
        spawnCountText.text = "";
    }

    private void YoureWinner(PlayerNumber playerNo) {
        winnerText.text = "게임종료!\n플레이어" + ((int)playerNo + 1) + "의 승리";
        ActiveRestartButton();
    }

    public void TimeOver() {
        winnerText.text = "시간종료!\n무승부";
        restartButton.onClick.RemoveAllListeners();
        ActiveRestartButton();
    }

    private void ActiveRestartButton() {
        restartButton.onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
        restartButton.gameObject.SetActive(true);
    }
}