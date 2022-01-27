using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : ObjectComponents, IMovable
{
    private float move_X, move_Y;
    private Vector2 moveDirection;
    [SerializeField] private float speed;
    private bool isMoving;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            ProcessInput();
        }
        else
        {
            //rigidBody.velocity = Vector2.zero;
            isMoving = false;
            move_Y = 0f;
        }
    }


    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            Move();
        }   
    }

    private void ProcessInput()
    {
        //move_X = Input.GetAxisRaw("Horizontal");
        move_Y = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(0, move_Y).normalized;
        isMoving = true;
    }

    public void Move()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, moveDirection.y * speed  * Time.fixedDeltaTime);
    }
}
