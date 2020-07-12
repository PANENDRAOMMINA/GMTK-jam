﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatJumper : MonoBehaviour
{
    [SerializeField] private float jumpFrequency;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpDecrier;
    [SerializeField] private float startingTimer;

    public float JumpTimer { get => jumpTimer; }

    private float jumpTimer;
    private Rigidbody2D moveRb;

    private void Start()
    {
        moveRb = GetComponent<Rigidbody2D>();
        jumpTimer = startingTimer;
    }

    private void FixedUpdate()
    {
        jumpTimer -= Time.deltaTime;

        if(moveRb.velocity.y < 0)
        {
            Vector2 vel = moveRb.velocity;
            vel.y *= jumpDecrier;
            moveRb.velocity = vel;
        }
        

        if(jumpTimer <= 0)
        {
            Jump();
            jumpTimer = jumpFrequency;
        }
    }

    private void Jump()
    {
        moveRb.velocity += Vector2.up * jumpForce;
    }
}