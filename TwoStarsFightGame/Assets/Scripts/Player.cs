using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public int health = 100;

    public Weapon currentWeapon = null;
    public PlayerNumber playerNo;

    private GameObject hpbar;
    public PlayerController playerController = null;
    public bool isAfterTime = false;

    [SerializeField]
    private Weapon defaultWeapon;

    public void Equip(Weapon weapon)
    {
        currentWeapon = weapon;
    }

    public bool DecreaseHP(int value)
    {
        if (health <= value) {
            health = 0;
            IngameUIManager.inst.UpdatePlayerHP(health, playerNo);
            GameManager.inst.PlayerDead(playerNo);
            return false;
        }
        health -= value;
        IngameUIManager.inst.UpdatePlayerHP(health, playerNo);
        return true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "DEADZONE") {
            GameManager.inst.PlayerDead(playerNo);
        }
    }
}
