using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default : Weapon, HandWeapon, Shield
{
    public override void AttackA()
    {
        skeleton.AnimationState.SetAnimation(1, "ATTACK_BASIC_1", false);
        equipPlayer.playerController.playerState = PlayerState.Attack;
        StartCoroutine(WaitTime(mode1Option.startTime, delegate { canDamage = true;
            StartCoroutine(WaitTime(mode1Option.animTime, delegate { canDamage = false; equipPlayer.isAfterTime = true;
                StartCoroutine(WaitTime(mode1Option.endTime, delegate { equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Idle;
                }));
            }));
        }));
    }

    public override void AttackB()
    {
        //skeleton.AnimationState.SetAnimation(1, "ATTACK_BASIC_2", false);
        equipPlayer.playerController.playerState = PlayerState.Attack;
        StartCoroutine(WaitTime(mode1Option.startTime, delegate {
            canDamage = true;
            StartCoroutine(WaitTime(mode1Option.animTime, delegate {
                canDamage = false; equipPlayer.isAfterTime = true;
                StartCoroutine(WaitTime(mode1Option.endTime, delegate { equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Idle;
                }));
            }));
        }));
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
        equipPlayer.playerController.playerState = PlayerState.Guard;
    }
    public override void DownAttackA()
    {
        skeleton.AnimationState.SetAnimation(1, "SHIELD_BASIC_2", false);
        StartCoroutine(WaitTime(mode2Option.startTime, delegate
        {
            equipPlayer.playerController.playerState = PlayerState.Parry;
            StartCoroutine(WaitTime(mode2Option.animTime, delegate
            {
                equipPlayer.isAfterTime = true;
                //equipPlayer.playerController.playerState = PlayerState.Attack;
                StartCoroutine(WaitTime(mode2Option.endTime, delegate
                {
                    equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Guard;
                }));
            }));
        }));
    }

    public override void DownAttackB()
    {
        skeleton.AnimationState.SetAnimation(1, "SHIELD_BASIC_3", false);
        equipPlayer.playerController.playerState = PlayerState.Rush;
        canDamage = true;
        StartCoroutine(WaitTime(mode2Option.startTime, delegate
        {
            //equipPlayer.transform.position += new Vector3(2f, 0f, 0f);
            //equipPlayer.rb.velocity += new Vector3(4f,0f,0f);
            StartCoroutine(WaitTime(mode2Option.animTime, delegate
            {
                canDamage = false; equipPlayer.isAfterTime = true;
                StartCoroutine(WaitTime(mode2Option.endTime, delegate
                {
                    equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Guard;
                }));
            }));
        }));
    }

    public override void DownAct()
    {
        if (equipPlayer.playerController.playerState != PlayerState.Guard && equipPlayer.playerController.playerState != PlayerState.Parry)
            skeleton.AnimationState.SetAnimation(1, "SHIELD_BASIC_1", false);
        Guard();
    }
}

