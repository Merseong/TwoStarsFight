using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default : Weapon, HandWeapon, Shield
{
    public override void AttackA()
    {
        skeleton.AnimationState.SetAnimation(1, "ATTACK_BASIC_1", false);
        StartCoroutine(WaitTime(mode1Option.startTime, delegate { canDamage = true;
            StartCoroutine(WaitTime(mode1Option.animTime, delegate { canDamage = false; equipPlayer.isAfterTime = true;
                StartCoroutine(WaitTime(mode1Option.endTime, delegate { equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Idle;
                }));
            }));
        }));
        Debug.Log("Default AttackA");
    }

    public override void AttackB()
    {
        //skeleton.AnimationState.SetAnimation(1, "ATTACK_BASIC_2", false);
        StartCoroutine(WaitTime(mode1Option.startTime, delegate {
            canDamage = true;
            StartCoroutine(WaitTime(mode1Option.animTime, delegate {
                canDamage = false; equipPlayer.isAfterTime = true;
                StartCoroutine(WaitTime(mode1Option.endTime, delegate { equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Idle;
                }));
            }));
        }));
        Debug.Log("Default AttackB");
        canDamage = true;
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
                equipPlayer.playerController.playerState=PlayerState.Parry;
                equipPlayer.isAfterTime = true;
                StartCoroutine(WaitTime(mode2Option.endTime, delegate
                {
                    equipPlayer.isAfterTime = false;
                    equipPlayer.playerController.playerState = PlayerState.Idle;
                }));
            }));
        }));
    }
}
