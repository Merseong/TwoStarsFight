using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yen : Weapon, RangeWeapon, HandWeapon
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

    public void Shoot(Vector2 start, Vector2 direction)
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

    public void HitAction()
    {

    }
}
