using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default : Weapon, HandWeapon, Shield
{

    public override void AttackA()
    {
        if (isModeChanged)
        {
            //true 모드의 A 공격
            Debug.Log("AttackTrue");

        }
        else
        {
            //false 모드의 A공격
            Debug.Log("AttackFalse");
        }
        
    }

    public override void AttackB()
    {
        if (isModeChanged)
        {
            //true 모드의 B 공격
            Debug.Log("Attack");
        }
        else
        {
            //false 모드의 B 공격
            Debug.Log("Attack");
        }
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
