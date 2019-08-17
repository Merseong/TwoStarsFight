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
    public Weapon euroWeapon;
    public bool isFlipped;

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
        playerController.playerKnockback(value);
        return true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("DEADZONE")) {
            GameManager.inst.PlayerDead(playerNo);
        }
        else if (other.CompareTag("WeaponBox"))
        {
            switch(other.GetComponent<WeaponBox>().inside)
            {
                case WeaponName.AT:
                    Equip(atWeapon, 100);
                    break;
                case WeaponName.EURO:
                    Equip(euroWeapon, 100);
                    break;
                case WeaponName.MALE:
                    Equip(marsWeapon, 100);
                    break;
                case WeaponName.YEN:
                    Equip(yenWeapon, 100);
                    break;
                default:
                    break;
            }
            other.GetComponent<WeaponBox>().parent.isWeaponSpawned = false;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Equip(euroWeapon, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            currentWeapon.Break();
        }
        if (transform.localScale.x < 0) {
            isFlipped = false;
        } else {
            isFlipped = true;
        }
    }

}
