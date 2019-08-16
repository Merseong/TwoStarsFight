using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Attack,
    Parry,
    Guard

}
public class PlayerController : MonoBehaviour
{
    
    public int playerControl;
    public Player player;
    public PlayerState playerState;

    //TODO: change variable type to private after valnace testing
    public float speed = 0f;
    public float jumpSpeed = 0f;
    private Collider2D col;
    private Transform landChecker;
    private float delayTimer = 0f;

    float vertical;
    float horizontal;
    float basicAttack;
    float specialAttack;
    float modeChange;
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
        playerState = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
        if (playerControl == 1)
        {
            vertical= Input.GetAxis("Vertical1P"); //up, down input
            horizontal = Input.GetAxis("Horizontal1P"); //left, right input

            if (Input.GetButtonDown("Jump1P")&&IsGround)
            {
                rb.velocity += new Vector2(0, jumpSpeed);
            }
            if (playerState != PlayerState.Attack)
            {
                if (Input.GetButtonDown("BasicAttack1P"))
                {
                    player.currentWeapon.AttackA();
                }
                if (Input.GetButtonDown("SpecialAttack1P"))
                {
                    player.currentWeapon.AttackB();
                }
                if (Input.GetButtonDown("ModeChange1P"))
                {
                    player.currentWeapon.ModeChange();
                }
                //guard and parry
                if (Input.GetButtonDown("Vertical1P"))
                {

                }

            }
        }
        else if(playerControl == 2)
        {
            vertical = Input.GetAxis("Vertical2P"); //up, down input
            horizontal = Input.GetAxis("Horizontal2P"); //left, right input

            if (Input.GetButtonDown("Jump2P") && IsGround)
            {
                rb.velocity += new Vector2(0, jumpSpeed);
            }
            else if (Input.GetButtonDown("BasicAttack2P"))
            {
                player.currentWeapon.AttackA();
            }
            else if (Input.GetButtonDown("SpecialAttack2P"))
            {
                player.currentWeapon.AttackB();
            }
            else if (Input.GetButtonDown("ModeChange2P"))
            {
                player.currentWeapon.ModeChange();
            }
        }
        else
        {
            Debug.Log("Player Parameter Error");
        }

        transform.position += new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;

        

    }
}
