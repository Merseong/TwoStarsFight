using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public PlayerNumber playerNo;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Body") && col.GetComponentInParent<Player>().playerNo != playerNo)
        {
            Destroy(gameObject, 10);
            col.GetComponentInParent<Player>().DecreaseHP(10);
        }
    }
}
