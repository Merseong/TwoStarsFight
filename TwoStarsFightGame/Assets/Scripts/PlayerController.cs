using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed=0f;
    public float jumpSpeed = 0f;
    public int player;


    private Collider2D col;
    private Transform landChecker;

    float vertical;
    float horizontal;
    float basicAttack;
    float specialAttack;
    float switchWeapon;
    public bool IsGround
    {
        get
        {
            return Physics2D.Linecast(landChecker.position + new Vector3(-col.bounds.size.x / 2 - 0.01f, 0),
                landChecker.position + new Vector3(col.bounds.size.x / 2 + 0.01f, 0),
                1 << LayerMask.NameToLayer("Ground")).transform != null;
        }
    }

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        landChecker = transform.Find("LandChecker");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == 1)
        {
            vertical= Input.GetAxis("Vertical1P"); //up, down input
            horizontal = Input.GetAxis("Horizontal1P"); //left, right input

            if (Input.GetButtonDown("Jump1P")&&IsGround)
            {
                rb.velocity += new Vector2(0, jumpSpeed);
            }
            else if (Input.GetButtonDown("BasicAttack1P"))
            {

            }
            else if (Input.GetButtonDown("SpecialAttack1P"))
            {

            }
            else if (Input.GetButtonDown("SwitchWeapon1P"))
            {

            }
        }
        else if(player == 2)
        {
            vertical = Input.GetAxis("Vertical2P"); //up, down input
            horizontal = Input.GetAxis("Horizontal2P"); //left, right input

            if (Input.GetButtonDown("Jump2P") && IsGround)
            {
                rb.velocity += new Vector2(0, jumpSpeed);
            }
            else if (Input.GetButtonDown("BasicAttack2P"))
            {

            }
            else if (Input.GetButtonDown("SpecialAttack2P"))
            {

            }
            else if (Input.GetButtonDown("SwitchWeapon2P"))
            {

            }
        }
        else
        {
            Debug.Log("Player Parameter Error");
        }

        transform.position += new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;

        

    }
}
