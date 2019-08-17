using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

// @
public class At : Weapon, HandWeapon, Shield
{
    public CircleCollider2D starCollider;
    public BoxCollider2D handCollider;

    public void Action()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackA()
    {
        if (isModeChanged)
        {
            skeleton.AnimationState.SetAnimation(1, "ATTACK_AT_1", false);
            StartCoroutine(WaitTime(mode1Option.startTime, delegate
            {
                canDamage = true;
                StartCoroutine(WaitTime(mode1Option.animTime, delegate
                {
                    canDamage = false; equipPlayer.isAfterTime = true;
                    StartCoroutine(WaitTime(mode1Option.endTime, delegate
                    {
                        equipPlayer.isAfterTime = false;
                        equipPlayer.playerController.playerState = PlayerState.Idle;
                    }));
                }));
            }));
        }
        else
        {
            equipPlayer.defaultWeapon.AttackA();
        }
    }

    public override void AttackB()
    {
        if (isModeChanged)
        {
            skeleton.AnimationState.SetAnimation(1, "ATTACK_AT_2", false);
            StartCoroutine(WaitTime(mode1Option.startTime, delegate {
                canDamage = true;
                StartCoroutine(WaitTime(mode1Option.animTime, delegate {
                    canDamage = false; equipPlayer.isAfterTime = true;
                    StartCoroutine(WaitTime(mode1Option.endTime, delegate {
                        equipPlayer.isAfterTime = false;
                        equipPlayer.playerController.playerState = PlayerState.Idle;
                    }));
                }));
            }));
        }
        else
        {
            equipPlayer.defaultWeapon.AttackB();
        }
    }

    public override void Break()
    {
        equipPlayer.Equip(equipPlayer.defaultWeapon, 100);
    }

    public override void DownAct()
    {
         equipPlayer.defaultWeapon.DownAct();
    }

    public override void DownAttackA()
    {
        equipPlayer.defaultWeapon.DownAttackA();
    }

    public override void DownAttackB()
    {
        equipPlayer.defaultWeapon.DownAttackB();
    }

    public void Guard()
    {
        return;
    }

    public override void ModeChange()
    {
        if (!isModeChanged)
        {
            isModeChanged = true;
            skeleton.AnimationState.SetAnimation(4, "WEAPON_" + mode2Option.weaponName, false);
            skeleton.AnimationState.SetEmptyAnimation(1, 0);
            handCollider.enabled = false;
            starCollider.enabled = true;
        }
        else
        {
            isModeChanged = false;
            skeleton.AnimationState.SetAnimation(4, "WEAPON_" + mode1Option.weaponName, false);
            skeleton.AnimationState.SetEmptyAnimation(1, 0);
            starCollider.enabled = false;
            handCollider.enabled = true;
        }
    }

    public void Parrying()
    {
        throw new System.NotImplementedException();
    }
}
