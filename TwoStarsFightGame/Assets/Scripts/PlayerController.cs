﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
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
    public SkeletonAnimation skeleton;
    public Player player;
    public float delayTimer = 0f;
    public PlayerState playerState = PlayerState.Idle;

    //TODO: change variable type to private after valnace testing
    public float speed = 0f;
    public float jumpSpeed = 0f;
    private Collider2D col;
    private Transform landChecker;
    private bool animSeted = false;
    

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
    void Awake()
    {
        landChecker = transform.Find("LandChecker");
        col = landChecker.GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
        vertical = Input.GetAxis("Vertical" + playerControl + "P"); //up, down input
        horizontal = Input.GetAxis("Horizontal" + playerControl + "P"); //left, right input
        if (playerState == PlayerState.Idle)
        {
            if (horizontal != 0 && !animSeted)
            {
                skeleton.AnimationState.SetAnimation(0, "RUN", true);
                animSeted = true;
            }
            else if (horizontal == 0 && animSeted)
            {
                skeleton.AnimationState.SetAnimation(0, "IDLE", true);
                animSeted = false;
            }
        }
        if (Input.GetButtonDown("Jump" + playerControl + "P") &&IsGround)
        {
            rb.velocity += new Vector2(0, jumpSpeed);
        }
        if (playerState != PlayerState.Attack)
        {
            if (Input.GetButtonDown("BasicAttack" + playerControl + "P"))
            {
                playerState = PlayerState.Attack;
                player.currentWeapon.AttackA();
            }
            if (Input.GetButtonDown("SpecialAttack" + playerControl + "P"))
            {
                playerState = PlayerState.Attack;
                player.currentWeapon.AttackB();
            }
            if (Input.GetButtonDown("ModeChange" + playerControl + "P"))
            {
                player.currentWeapon.ModeChange();
            }
            //guard and parry
            if (vertical < 0)
            {
                player.currentWeapon.DownAct();
                playerState = PlayerState.Guard;
                if (Input.GetButtonDown("BasicAttack" + playerControl + "P"))
                {
                    playerState = PlayerState.Attack;
                    player.currentWeapon.DownAttackA();
                }
                if (Input.GetButtonDown("SpecialAttack" + playerControl + "P"))
                {
                    playerState = PlayerState.Attack;
                    player.currentWeapon.DownAttackB();
                }
            }
        }
        if (playerState == PlayerState.Guard && vertical >= 0)
        {
            skeleton.AnimationState.AddAnimation(0, "IDLE", true, 0);
            skeleton.AnimationState.SetEmptyAnimation(1, 0);
            playerState = PlayerState.Idle;
        }
        if (playerState != PlayerState.Guard) transform.position += new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;
    }
}
