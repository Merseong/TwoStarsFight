﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMark : Weapon, HandWeapon
{
    public void Action()
    {

    }

    public override void AttackA()
    {

    }

    public override void AttackB()
    {
        throw new System.NotImplementedException();
    }

    public override void Break()
    {
        equipPlayer.currentWeapon = this as Weapon;
    }

    public void Guard()
    {
        throw new System.NotImplementedException();
    }

    public override void ModeChange()
    {
        return;
    }

    public void Parrying()
    {
        throw new System.NotImplementedException();
    }
}