using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : ObjectComponents, IMovable
{
    private float move_X, move_Y;


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
        move_X = Input.GetAxisRaw("Horizontal");
        move_Y = Input.GetAxisRaw("Vertical");
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }
}
