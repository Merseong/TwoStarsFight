using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public PlayerNumber playerNo;
    private Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Body") && col.GetComponentInParent<Player>().playerNo != playerNo)
        {
            Destroy(gameObject, 10);
            col.GetComponentInParent<Player>().DecreaseHP(10);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, 360.0f - Vector3.Angle(this.transform.right, this.rb.velocity.normalized)));
    }
}
