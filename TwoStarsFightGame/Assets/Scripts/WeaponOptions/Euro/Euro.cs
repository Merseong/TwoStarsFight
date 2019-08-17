using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Euro : Weapon, RangeWeapon, HandWeapon
{
    public override void AttackA()
    {
            skeleton.AnimationState.SetAnimation(1, "ATTACK_BASIC_1", false);
            StartCoroutine(WaitTime(mode1Option.startTime, delegate
            {
                canDamage = true;
                StartCoroutine(WaitTime(mode1Option.animTime, delegate
                {
                    Shoot(ShotPosition.position, Direction);
                    canDamage = false; equipPlayer.isAfterTime = true;
                    StartCoroutine(WaitTime(mode1Option.endTime, delegate
                    {
                        equipPlayer.isAfterTime = false;
                        equipPlayer.playerController.playerState = PlayerState.Idle;
                    }));
                }));
            }));
            Debug.Log("Euro AttackA");
        
    }

    public override void AttackB()
    {
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
        Debug.Log("Euro AttackB");
    }

    public override void Break()
    {
        equipPlayer.currentWeapon = this as Weapon;
    }
    public override void ModeChange()
    {
        isModeChanged = !isModeChanged;
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

    }

    public void Parrying()
    {
        //mode2Option == information of default shield
        //start delay -> parrying -> end delay
        //skeleton.AnimationState.SetAnimation(1, "SHIELD_BASIC_1", false);
        StartCoroutine(WaitTime(mode2Option.startTime, delegate
        {
            StartCoroutine(WaitTime(mode2Option.animTime, delegate
            {
                equipPlayer.playerController.playerState = PlayerState.Parry;
                equipPlayer.isAfterTime = true;
                StartCoroutine(WaitTime(mode2Option.endTime, delegate
                {
                    equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Idle;
                }));
            }));
        }));
    }
    public override void DownAttackA()
    {
        skeleton.AnimationState.SetAnimation(1, "SHIELD_BASIC_2", false);
        StartCoroutine(WaitTime(mode2Option.startTime, delegate
        {
            StartCoroutine(WaitTime(mode2Option.animTime, delegate
            {
                equipPlayer.playerController.playerState = PlayerState.Parry;
                equipPlayer.isAfterTime = true;
                StartCoroutine(WaitTime(mode2Option.endTime, delegate
                {
                    equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Idle;
                }));
            }));
        }));
    }

    public override void DownAttackB()
    {
        skeleton.AnimationState.SetAnimation(1, "SHIELD_BASIC_3", false);
        StartCoroutine(WaitTime(mode2Option.startTime, delegate
        {
            canDamage = true;
            StartCoroutine(WaitTime(mode2Option.animTime, delegate
            {
                canDamage = false; equipPlayer.isAfterTime = true;
                StartCoroutine(WaitTime(mode2Option.endTime, delegate
                {
                    equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Idle;
                }));
            }));
        }));
    }

    public override void DownAct()
    {
        if (equipPlayer.playerController.playerState != PlayerState.Guard)
            skeleton.AnimationState.SetAnimation(1, "SHIELD_BASIC_1", false);
        Guard();
    }

    public void Shoot(Vector2 start, Vector2 direction)
    {
        Arrow = Instantiate(ArrowPrefab, start, Quaternion.identity);
        Arrow.GetComponent<Rigidbody2D>().velocity = direction * 20f;
    }

    public void HitAction()
    {

    }
}

