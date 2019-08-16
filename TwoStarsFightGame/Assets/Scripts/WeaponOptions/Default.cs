using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default : Weapon, HandWeapon, Shield
{

    public override void AttackA()
    {
        Debug.Log("Default AttackA");
        canDamage = true;
    }

    public override void AttackB()
    {
        Debug.Log("Default AttackB");
        canDamage = true;
    }

    public override void Break()
    {
        equipPlayer.currentWeapon = this as Weapon;
    }
    public override void ModeChange()
    {
        this.isModeChanged = !this.isModeChanged;
        return;
    }

    public void Action()
    {
        Debug.Log("Action");
    }
    public void SpecialAction()
    {
        Debug.Log("SpecialAction");
    }

    public void Guard()
    {
        Debug.Log("Guard");
    }

    public void Parrying()
    {
        Debug.Log("Parrying");
    }
}
