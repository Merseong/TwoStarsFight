using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Euro : Weapon, RangeWeapon, HandWeapon
{
    public CircleCollider2D defaultCol;
    public override void AttackA()
    {
        if (isModeChanged)
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
        else
        {
            skeleton.AnimationState.SetAnimation(1, "ATTACK_EURO_3", false);
            equipPlayer.playerController.playerState = PlayerState.Attack;
            StartCoroutine(WaitTime(mode2Option.startTime, delegate
            {
                canDamage = true;
                DecreaseDurability(mode2Option.minusDurability);
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
        
    }

    public override void AttackB()
    {
        if (isModeChanged)
        {
            skeleton.AnimationState.SetAnimation(1, "ATTACK_EURO_2", false);
            equipPlayer.playerController.playerState = PlayerState.Attack;
            StartCoroutine(WaitTime(mode1Option.startTime, delegate
            {

                canDamage = true;
                StartCoroutine("ArrowRain");
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
            skeleton.AnimationState.SetAnimation(1, "ATTACK_EURO_4", false);
            equipPlayer.playerController.playerState = PlayerState.Attack;
            StartCoroutine(WaitTime(mode2Option.startTime, delegate
            {

                canDamage = true;
                if (isFlipped) boomerangShoot(new Vector2(1, 0));
                else boomerangShoot(new Vector2(-1, 0));

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
            Debug.Log("Euro AttackB");
        }
    }

    public override void Break()
    {
        equipPlayer.Equip(equipPlayer.defaultWeapon, 100);
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

    public override void DownAttackA()
    {
        equipPlayer.defaultWeapon.DownAttackA();
    }

    public override void DownAttackB()
    {
        equipPlayer.defaultWeapon.DownAttackB();
    }


    public override void DownAct()
    {
        equipPlayer.defaultWeapon.DownAct();
    }

    public void Shoot(Vector2 start, Vector2 direction)
    {
        DecreaseDurability(mode1Option.minusDurability);
        Arrow = Instantiate(ArrowPrefab, start, Quaternion.Euler(0,0,90));
        Arrow.GetComponent<Rigidbody2D>().AddForce(direction * 20f, ForceMode2D.Impulse);
        Arrow.GetComponent<Arrow>().playerNo = equipPlayer.playerNo;
    }

    public void Shoot(Vector2 start, Vector2 direction, float speed)
    {
        DecreaseDurability(mode1Option.minusDurability);
        Arrow = Instantiate(ArrowPrefab, start, Quaternion.identity);
        Arrow.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
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

    public void boomerangShoot(Vector2 direction)
    {
        DecreaseDurability(mode2Option.minusDurability);
        Boomerang = Instantiate(BoomerangPrefab, ShotPosition.position, Quaternion.identity);
        Boomerang.GetComponent<Rigidbody2D>().velocity = direction * 20f;
        Boomerang.GetComponent<Boomerang>().playerNo = equipPlayer.playerNo;
        Boomerang.GetComponent<Boomerang>().direction = direction;
    }
}

