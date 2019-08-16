using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;

    public bool DecreaseHP(int value)
    {
        if (health < value)
        {
            health = 0;
            return false;
        }
        else
        {
            health -= value;
            return true;
        }
    }
}
