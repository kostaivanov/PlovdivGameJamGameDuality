using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : ObjectComponents, IMovable
{
    private float move_X, move_Y;
    private Vector2 moveDirection;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            ProcessInput();
        }
    }

    private void ProcessInput()
    {
        //move_X = Input.GetAxisRaw("Horizontal");
        move_Y = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(move_X, move_Y).normalized;

    }

    public void Move()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, moveDirection.y * moveSpeed * dash * Time.fixedDeltaTime);
    }
}
