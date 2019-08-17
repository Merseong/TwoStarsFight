using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarsSymbol : Weapon, HandWeapon
{
    public Collider2D defaultCol;
    private Vector2 default2Size = new Vector2(0.78f, 0.13f);
    public void Action()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackA()
    {
        if (!isModeChanged)
        {
            defaultCol.enabled = true;
            DecreaseDurability(mode1Option.minusDurability);
            skeleton.AnimationState.SetAnimation(1, "ATTACK_MALE_1", false);
            equipPlayer.playerController.playerState = PlayerState.Attack;
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
            Debug.Log("_YEN AttackA");
        }
        else
        {
            DecreaseDurability(mode2Option.minusDurability);
            skeleton.AnimationState.SetAnimation(1, "ATTACK_MALE_3", false);
            equipPlayer.playerController.playerState = PlayerState.Attack;
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
    }

    public override void AttackB()
    {
        if (!isModeChanged)
        {
            defaultCol.enabled = true;
            DecreaseDurability(mode1Option.minusDurability);
            skeleton.AnimationState.SetAnimation(1, "ATTACK_MALE_2", false);
            equipPlayer.playerController.playerState = PlayerState.Attack;
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
            Debug.Log("Euro AttackB");
        }
        else
        {
            DecreaseDurability(mode2Option.minusDurability);
            skeleton.AnimationState.SetAnimation(1, "ATTACK_MALE_4", false);
            equipPlayer.playerController.playerState = PlayerState.Attack;
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

    public override void ModeChange()
    {
        if (!isModeChanged)
        {
            isModeChanged = true;
            skeleton.AnimationState.SetAnimation(4, "WEAPON_" + mode2Option.weaponName, false);
            skeleton.AnimationState.SetEmptyAnimation(1, 0);
            equipPlayer.OffAllCol();
            defaultCol.enabled = true;
            defaultCol.transform.localScale = default2Size;
        }
        else
        {
            isModeChanged = false;
            skeleton.AnimationState.SetAnimation(4, "WEAPON_" + mode1Option.weaponName, false);
            skeleton.AnimationState.SetEmptyAnimation(1, 0);
            equipPlayer.OffAllCol();
            defaultCol.enabled = true;
            defaultCol.transform.localScale = new Vector2(1, 1);
        }
        return;
    }
}
