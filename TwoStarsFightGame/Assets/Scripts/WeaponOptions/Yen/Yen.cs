using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yen : Weapon, RangeWeapon, HandWeapon
{
    public CapsuleCollider2D defaultCol;
    public override void AttackA()
    {
        if (!isModeChanged)
        {
            defaultCol.enabled = true;
            DecreaseDurability(mode1Option.minusDurability);
            skeleton.AnimationState.SetAnimation(1, "ATTACK_YEN_1", false);
            equipPlayer.playerController.playerState = PlayerState.Attack;
            StartCoroutine(WaitTime(mode1Option.startTime, delegate
            {
                canDamage = true;

                if (isFlipped) Shoot(ShotPosition.position, new Vector2(1, 0.3f));
                else Shoot(ShotPosition.position, new Vector2(-1, 0.3f));

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
            skeleton.AnimationState.SetAnimation(1, "ATTACK_YEN_3", false);
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
            skeleton.AnimationState.SetAnimation(1, "ATTACK_YEN_2", false);
            equipPlayer.playerController.playerState = PlayerState.Attack;
            StartCoroutine(WaitTime(mode1Option.startTime, delegate
            {

                canDamage = true;
                if (isFlipped) Shoot(ShotPosition.position, new Vector2(1, 0));
                else Shoot(ShotPosition.position, new Vector2(-1, 0));

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
            skeleton.AnimationState.SetAnimation(1, "ATTACK_YEN_4", false);
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
    public override void ModeChange()
    {
        if (!isModeChanged)
        {
            isModeChanged = true;
            skeleton.AnimationState.SetAnimation(4, "WEAPON_" + mode2Option.weaponName, false);
            skeleton.AnimationState.SetEmptyAnimation(1, 0);
            equipPlayer.OffAllCol();
            transform.SetParent(equipPlayer.yenSpear);
            transform.localPosition = Vector3.zero;
            defaultCol.enabled = true;
        }
        else
        {
            isModeChanged = false;
            skeleton.AnimationState.SetAnimation(4, "WEAPON_" + mode1Option.weaponName, false);
            skeleton.AnimationState.SetEmptyAnimation(1, 0);
            equipPlayer.OffAllCol();
            transform.SetParent(equipPlayer.yenCrossBow);
            transform.localPosition = Vector3.zero;
        }
        return;
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

    public void HitAction()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot(Vector2 start, Vector2 direction)
    {
        Arrow = Instantiate(ArrowPrefab, start, Quaternion.identity);
        Arrow.GetComponent<Rigidbody2D>().AddForce(direction * 20f, ForceMode2D.Impulse);
        Arrow.GetComponent<Arrow>().playerNo = equipPlayer.playerNo;
    }

    public void Action()
    {
        throw new System.NotImplementedException();
    }
}
