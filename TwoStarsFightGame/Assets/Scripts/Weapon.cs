using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    NULL,
    HandWeapon,
    Range,
    Shield
}

public enum WeaponState
{
    Idle,
    Start,
    Attack,
    End
}

public abstract class Weapon : MonoBehaviour
{
    [Header("When Item Mode")]
    public bool isItem = true;

    [Header("When Weapon Mode")]
    public WeaponState weaponState = WeaponState.Idle;
    public bool isModeChanged = false;
    public Player equipPlayer = null;
    public int durability = 100;
    public WeaponOption mode1Option;
    public WeaponOption mode2Option;
    public float startTimer = 0f;
    public float endTimer = 0f;

    [Space(10)]
    private Coroutine currentBreakCount;
    private Coroutine attackStartCount;
    private Coroutine attackEndCount;

    public void Update()
    {
        //if(equipPlayer!=null)
            //transform.position = equipPlayer.transform.position;
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (isItem)
        {
            Debug.Log("OntriggerOn");
            isItem = false;
            //GetComponent<Collider2D>().enabled = false;
            equipPlayer = col.GetComponent<Player>();
            equipPlayer.Equip(this);
            currentBreakCount = StartCoroutine(ItemBreakCount());
        }
        else
        {
            if (col.GetComponentInParent<Player>().playerNumber != equipPlayer.playerNumber)
            {
                Debug.Log("Damaged");
                col.GetComponentInParent<Player>().DecreaseHP(mode1Option.damage);
            }
        }

    }

    public void DropWeapon()
    {
        StopCoroutine(currentBreakCount);
        equipPlayer = null;
        //GetComponent<Collider>().enable = true;
        isItem = true;
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

    IEnumerator WeaponStartCount()
    {
        while (startTimer >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            startTimer -= 0.1f;
        }
        Break();
    }

    IEnumerator WeaponEndCount()
    {
        while (endTimer >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            endTimer -= 0.1f;
        }
        Break();
    }

    public abstract void Break();
    public abstract void ModeChange();
    public abstract void AttackA();
    public abstract void AttackB();
}

[CreateAssetMenu]
public class WeaponOption : ScriptableObject
{
    [Header("무기의 옵션들")]
    public string weaponName;
    public WeaponType weaponType;
    [Tooltip("무기의 데미지")]
    public int damage;
    [Tooltip("공격시 감소하는 내구도")]
    public int minusDurability;
    [Tooltip("넉백 시간")]
    public float knockbackTime;
    [Tooltip("경직 시간")]
    public float stiffTime;
    [Tooltip("신기한 액션 여부")]
    public bool haveAction;
    [Header("공격자의 옵션들")]
    [Tooltip("선딜")]
    public float startTime;
    [Tooltip("후딜")]
    public float endTime;
    [Tooltip("쿨타임")]
    public float coolTime;
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
    void Parrying();
}