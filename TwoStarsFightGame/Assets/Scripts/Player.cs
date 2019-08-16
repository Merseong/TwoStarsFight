﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public int health = 100;
    public Weapon currentWeapon = null;
    public PlayerController playerController = null;

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
            return false;
        }
        else
        {
            health -= value;
            return true;
        }
    }
}
