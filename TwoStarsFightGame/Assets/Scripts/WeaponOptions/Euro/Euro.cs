using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Euro : Weapon, RangeWeapon, HandWeapon
{
    public CircleCollider2D defaultCol;
    public override void AttackA()
    {
        skeleton.AnimationState.SetAnimation(1, "ATTACK_EURO_1", false);
        equipPlayer.playerController.playerState = PlayerState.Attack;
        StartCoroutine(WaitTime(mode1Option.startTime, delegate
            {
                canDamage = true;

                StartCoroutine(WaitTime(mode1Option.animTime, delegate
                {
                    canDamage = false; equipPlayer.isAfterTime = true;

                    if (isFlipped) Shoot(ShotPosition.position, new Vector2(1, 0));
                    else Shoot(ShotPosition.position, new Vector2(-1, 0));
                    
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
        skeleton.AnimationState.SetAnimation(1, "ATTACK_EURO_2", false);
        equipPlayer.playerController.playerState = PlayerState.Attack;
        StartCoroutine(WaitTime(mode1Option.startTime, delegate {
            
            canDamage = true;
            StartCoroutine("ArrowRain");
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
        equipPlayer.playerController.playerState = PlayerState.Attack;
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
        equipPlayer.playerController.playerState = PlayerState.Attack;
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
        Arrow = Instantiate(ArrowPrefab, start, Quaternion.Euler(0,0,90));
        Arrow.GetComponent<Rigidbody2D>().velocity = direction * 20f;
        Arrow.GetComponent<Arrow>().playerNo = equipPlayer.playerNo;
    }

    public void Shoot(Vector2 start, Vector2 direction, float speed)
    {
        Arrow = Instantiate(ArrowPrefab, start, Quaternion.identity);
        Arrow.GetComponent<Rigidbody2D>().velocity = direction * speed;
        Arrow.GetComponent<Arrow>().playerNo = equipPlayer.playerNo;
    }

    public void HitAction()
    {

    }

    IEnumerator ArrowRain()
    {
        for(int i = 0; i<6;i++)
        {
            yield return new WaitForSeconds(0.1f);
            Shoot(ShotPosition.position, Quaternion.Euler(0,0,Random.Range(-50f,50f)) * new Vector2(0, 1),10f);
        }
        
    }
}

