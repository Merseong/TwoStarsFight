using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public int health = 100;

    public Weapon currentWeapon = null;
    public PlayerNumber playerNo;

    public bool playertype;

    private GameObject hpbar;
    public PlayerController playerController = null;

    [SerializeField]
    private Weapon defaultWeapon;

    public void OnEnable()
    {
        if (playertype)
        {
            hpbar = GameObject.Find("1P_HP_Bar");
            for (int i = 0; i < 100; i++) // 플레이어 리스폰 시 hp바 초기화
                IngameUIManager.inst.Player1_hp[i] = Instantiate(IngameUIManager.inst.hpPrefab, hpbar.transform);
        }
        else
        {
            hpbar = GameObject.Find("2P_HP_Bar");
            for (int i = 0; i < 100; i++)
                IngameUIManager.inst.Player2_hp[i] = Instantiate(IngameUIManager.inst.hpPrefab, hpbar.transform);
        }

    }

    public void Update()
    {
        IngameUIManager.inst.UpdatePlayerHP(health, playertype);
    }

    public void Equip(Weapon weapon)
    {
        currentWeapon = weapon;
    }

    public bool DecreaseHP(int value)
    {
        if (health < value)
        {
            health = 0;
            GameManager.inst.PlayerDead(playerNo);
            return false;
        }
        else
        {
            health -= value;
            return true;
        }
    }
}
