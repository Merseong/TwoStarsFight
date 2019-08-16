using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("When Item Mode")]
    public bool isItem = true;

    [Header("When Weapon Mode")]
    public bool isModeChanged = false;
    public int durability = 100;
    public WeaponOption mode1Option;
    public WeaponOption mode2Option;

    [Space(10)]
    private Coroutine currentBreakCount;

    public void OnTriggerEnter(Collider col)
    {
        /*
         * isItem = false;
         * GetComponent<Collider>().enable = false;
         * col.GetComponent<Player>().Equip(this);
         * currentBreakCount = StartCoroutine(ItemBreakCount());
         * */
    }

    public void DropWeapon()
    {
        StopCoroutine(currentBreakCount);
        //GetComponent<Collider>().enable = true;
        isItem = true;
    }

    public void DecreaseDurability()
    {

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
    [Tooltip("무기의 데미지")]
    public float damage;
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