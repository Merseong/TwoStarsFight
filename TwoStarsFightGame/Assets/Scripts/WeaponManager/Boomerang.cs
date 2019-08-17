using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerNumber playerNo;
    public float time;
    public Vector2 direction;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Body") && col.GetComponentInParent<Player>().playerNo != playerNo)
        {
            col.GetComponentInParent<Player>().DecreaseHP(10);
        }
    }

    public void Update()
    {
        time += Time.deltaTime;
        if(time > 0.01)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 20);
            rb.AddForce(direction * -10);
            time -= 0.01f;
        }
        if (rb.velocity.magnitude > 20f)
            Destroy(gameObject);
    }
}
