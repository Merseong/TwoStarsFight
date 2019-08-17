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

    [Header("Weapons in body")]
    public Weapon defaultWeapon;
    public Weapon atWeapon;
    public Weapon marsWeapon;
    public Weapon yenWeapon;

    public void Equip(Weapon weapon, int durability)
    {   
        currentWeapon = weapon;
        currentWeapon.OnEquip(durability);
        weapon.skeleton = playerController.skeleton;
        playerController.skeleton.AnimationState.SetAnimation(4, "WEAPON_" + weapon.mode1Option.weaponName, false);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Equip(atWeapon, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            currentWeapon.Break();
        }
    }
}
