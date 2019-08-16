using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public Weapon currentWeapon = null;
    public PlayerNumber playerNo;

    [SerializeField]
    private Weapon defaultWeapon;

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
