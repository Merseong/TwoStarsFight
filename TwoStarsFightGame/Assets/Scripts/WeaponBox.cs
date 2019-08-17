using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName
{
    BASIC,
    AT,
    EURO,
    MALE,
    YEN
}

public class WeaponBox : MonoBehaviour
{
    public ItemSpawner parent;
    public WeaponName inside;
}
