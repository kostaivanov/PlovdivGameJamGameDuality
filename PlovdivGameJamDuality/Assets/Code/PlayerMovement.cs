using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : ObjectComponents, IMovable
{
    private const float minimumFallingVelocity_Y = -2f;

    private float extraDistance = 0.1f;

    [SerializeField] private float jumpForce;
    [SerializeField] private float decayRate;

    private float jumpForce_2;

    private bool jumpPressed;
    //private bool jumpHolded;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    internal PlayerState state = PlayerState.running;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //isMoving = false;
        jumpPressed = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }


    private void FixedUpdate()
    {
        //if (isMoving == true)
        //{
        //    Move();
        //}

        if (jumpPressed == true && CheckIfIsGrounded() == true)
        {
            StartCoroutine(Jump());

            jumpPressed = false;
        }
        if (!CheckIfIsGrounded())
        {
            jumpPressed = false;
        }
    }

    private void LateUpdate()
    {
        this.AnimationStateSwitch();
        base.animator.SetInteger("state", (int)state);
    }

    internal bool CheckIfIsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(base.collider2D.bounds.center, base.collider2D.bounds.size, 0f, Vector2.down, extraDistance, base.groundLayer);

        return rayCastHit.collider != null;
    }

    public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    private IEnumerator Jump()
    {
        jumpForce_2 = CalculateJumpForce(Physics2D.gravity.magnitude, jumpForce);

        rigidBody.AddForce(Vector2.up * (jumpForce_2 * 2) * rigidBody.mass);
        yield return null;

        //can be any value, maybe this is a start ascending force, up to you
        float currentForce = jumpForce_2;

        while (Input.GetKey(KeyCode.Space) && currentForce > 0)
        {
            rigidBody.AddForce(Vector2.up * currentForce * rigidBody.mass);
            currentForce -= decayRate * Time.fixedDeltaTime;
            yield return null;
        }
    }

    protected void AnimationStateSwitch()
    {

        if (rigidBody.velocity.y > 1f && CheckIfIsGrounded() != true)
        {
            this.state = PlayerState.jumping;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("falling") && state == PlayerState.falling && collider2D.IsTouchingLayers(groundLayer))
        {
            state = PlayerState.landing;
        }
        else if (state == PlayerState.jumping)
        {
            if (rigidBody.velocity.y == 0 || CheckIfIsGrounded() == true)
            {
                state = PlayerState.running;
            }
        }
        else if (state == PlayerState.jumping)
        {

            if (rigidBody.velocity.y < minimumFallingVelocity_Y)
            {
                state = PlayerState.falling;
            }
        }
        else if (state == PlayerState.falling)
        {
            if (collider2D.IsTouchingLayers(groundLayer))
            {
                state = PlayerState.running;
            }
        }
        else
        {
            state = PlayerState.running;
        }

        if (rigidBody.velocity.y < minimumFallingVelocity_Y)
        {
            state = PlayerState.falling;
        }
    }

    //private void FootStep()
    //{
    //    footStep.Play();
    //}

    public void Move()
    {
        throw new NotImplementedException();
    }

}
