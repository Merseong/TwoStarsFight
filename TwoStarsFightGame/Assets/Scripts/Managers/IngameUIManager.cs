using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameUIManager : SingletonBehaviour<IngameUIManager>
{
    public Text playtimeText;
    public Text p1WeaponDurability, p2WeaponDurability;
    public GameObject hpPrefabRight;
    public GameObject hpPrefabLeft;

    public Image[,] Player_hp = new Image[2,100];

    private int isGenerated = 0;

    public void ResetHPBar(PlayerNumber pnum)
    {
        GameObject hpbar;

        hpbar = GameObject.Find(((int)pnum + 1) + "P_HP_Bar");
        if (isGenerated < 2)
        {
            for (int i = 0; i < 100; i++) // 플레이어 리스폰 시 hp바 초기화
                Player_hp[(int)pnum, i] = Instantiate(pnum == PlayerNumber.player1 ? hpPrefabLeft : hpPrefabRight, hpbar.transform).GetComponent<Image>();
            isGenerated++;
        }
        else
        {
            for (int i = 0; i < 100; i++) // 플레이어 리스폰 시 hp바 초기화
                Player_hp[(int)pnum, i].enabled = true;
        }
    }

    public void UpdatePlaytimeText(float time)
    {
        string seconds = time.ToString("00");
        playtimeText.text = seconds;
    }

    public void UpdatePlayerHP(int hp, PlayerNumber pNum)
    {
        if (pNum == PlayerNumber.player1)
        {
            for(int i = 0; i< 100; i++)
            {
                if( i < 100 - hp)
                    Player_hp[(int)pNum, i].enabled = false;
            }
                
        }
        else
        {
            for (int i = 0; i < 100; i++)
            {
                if (i >=  hp)
                    Player_hp[(int)pNum, i].enabled = false;
            }

        }
    }
    void Update() {
        p1WeaponDurability.text = "";
        p2WeaponDurability.text = "";
        int p1wd = GameManager.inst.currentPlayer[0].currentWeapon.durability / 10;
        int p2wd = GameManager.inst.currentPlayer[1].currentWeapon.durability / 10;
        for (int i = 0; i < p1wd; i++) p1WeaponDurability.text += ">";
        for (int i = 0; i < p2wd; i++) p2WeaponDurability.text += "<";
    }
}
