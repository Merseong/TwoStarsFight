using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum WeaponType
{
    NULL,
    HandWeapon,
    Range,
    Shield
}

/*public enum WeaponState
{
    Idle,
    Start,
    Attack,
    End
}*/

public abstract class Weapon : MonoBehaviour
{
    [Header("When Weapon Mode")]
    //public WeaponState weaponState = WeaponState.Idle;
    public WeaponType weaponType;
    public bool isModeChanged = false;
    public Player equipPlayer = null;
    public int durability = 100;
    public WeaponOption mode1Option;
    public WeaponOption mode2Option;
    public float startTimer = 0f;
    public float endTimer = 0f;
    public SkeletonAnimation skeleton;

    public GameObject ArrowPrefab;
    public GameObject Arrow;
    public GameObject BoomerangPrefab;
    public GameObject Boomerang;
    public Transform ShotPosition;
    public bool isFlipped;


    [Space(10)]
    private Coroutine currentBreakCount;
    private Coroutine attackStartCount;
    private Coroutine attackEndCount;

    // 선딜 후 canDamage를 true로 만듬
    // 타격 성공시 canDamage를 false로 만듬
    // 후딜 시작시 canDamage를 false로 만듬
    [SerializeField]
    protected bool canDamage = false;
    protected bool canParry = false;
    protected bool canGuard = false;


    public void Update()
    {
        //if(equipPlayer!=null)
        //transform.position = equipPlayer.transform.position;
        isFlipped = equipPlayer.isFlipped;
    }
    protected void OnTriggerEnter2D(Collider2D col)
    {
        //if (isItem)
        //{
        //    Debug.Log("OntriggerOn");
        //    isItem = false;
        //    //GetComponent<Collider2D>().enabled = false;
        //    equipPlayer = col.GetComponent<Player>();
        //    equipPlayer.Equip(this);
        //    
        //}
        //else 이 윗부분은 따로 프리팹으로 만들어야될듯
        if (canDamage)
        {
            if (col.CompareTag("Body") && col.GetComponentInParent<Player>().playerNumber != equipPlayer.playerNumber)
            {
                if (col.GetComponentInParent<Player>().playerController.playerState == PlayerState.Parry)
                {
                    equipPlayer.playerController.playerState = PlayerState.Stern;
                    StartCoroutine(WaitTime(mode2Option.stiffTime, delegate
                    {
                        equipPlayer.playerController.playerState = PlayerState.Idle;
                    }));
                }
                else if (col.GetComponentInParent<Player>().playerController.playerState == PlayerState.Guard)
                {
                    col.GetComponentInParent<Player>().DecreaseHP(mode1Option.damage - 5);
                }
                else
                {
                    if (equipPlayer.playerController.playerState != PlayerState.Rush)
                    {
                        col.GetComponentInParent<Player>().DecreaseHP(mode1Option.damage);
                        col.GetComponentInParent<Player>().playerController.playerState = PlayerState.Stern;
                        StartCoroutine(WaitTime(mode1Option.stiffTime, delegate
                        {
                            col.GetComponentInParent<Player>().playerController.playerState = PlayerState.Idle;
                        }));
                    }
                    else
                    {
                        col.GetComponentInParent<Player>().playerController.playerState = PlayerState.Stern;
                        StartCoroutine(WaitTime(mode2Option.stiffTime, delegate
                        {
                            col.GetComponentInParent<Player>().playerController.playerState = PlayerState.Idle;
                        }));
                    }
                    canDamage = false;
                }
            }
        }
    }

    public void OnEquip(int dura)
    {
        durability = dura;
        if (this is MarsSymbol)
        {
            transform.SetParent(equipPlayer.marsSpear);
            transform.localPosition = Vector3.zero;
        }
        else if (this is Yen)
        {
            transform.SetParent(equipPlayer.yenCrossBow);
            transform.localPosition = Vector3.zero;
        }
        currentBreakCount = StartCoroutine(ItemBreakCount());
    }

    public void DecreaseDurability(int value)
    {
        if (durability < value)
        {
            StopCoroutine(currentBreakCount);
            Break();
        }
        else
        {
            durability -= value;
        }
    }

    IEnumerator ItemBreakCount()
    {
        while (durability > 0)
        {
            yield return new WaitForSeconds(1);
            durability--;
        }
        Break();
    }

    protected delegate void callback();
    protected IEnumerator WaitTime(float time, callback _callback)
    {
        yield return new WaitForSeconds(time);
        _callback();
    }


    public abstract void Break();
    public abstract void ModeChange();
    public abstract void AttackA();
    public abstract void AttackB();
    public abstract void DownAct();
    public abstract void DownAttackA();
    public abstract void DownAttackB();
}
public interface RangeWeapon
{
    void Shoot(Vector2 start, Vector2 direction);
    void HitAction();
}

public interface HandWeapon
{
    void Action();
}

public interface Shield
{
    void Guard();
}