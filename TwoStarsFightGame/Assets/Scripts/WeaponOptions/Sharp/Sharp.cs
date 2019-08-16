using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : Weapon, HandWeapon, Shield
{
    public void Action()
    {

    }

    public override void AttackA()
    {

    }

    public override void AttackB()
    {

    }

    public override void Break()
    {
        equipPlayer.currentWeapon = this as Weapon;
    }

    public void Guard()
    {
 
    }

    public override void ModeChange()
    {
        return;
    }

    public void Parrying()
    {

    }
}
