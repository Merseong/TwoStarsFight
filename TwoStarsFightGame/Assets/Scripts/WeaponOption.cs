using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Tooltip("애니메이션 타임")]
    public float _animTime;
    public float animTime { get { return _animTime - (startTime + endTime); } }
    [Tooltip("후딜")]
    public float endTime;
    [Tooltip("쿨타임")]
    public float coolTime;
}
