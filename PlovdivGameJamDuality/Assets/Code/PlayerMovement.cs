using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : ObjectComponents, IMovable
{
    private const float minFreeFallStateVelocity_Y = -2.1f;
    private const float minFallAfterJumpVelocity_Y = 0.1f;

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
        //the initial jump
        Debug.Log(jumpForce_2);
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

    private void AnimationStateSwitch()
    {
        if (state == PlayerState.jumping)
        {
            if (rigidBody.velocity.y < minFallAfterJumpVelocity_Y)
            {
                state = PlayerState.falling;
            }
        }

        else if (state == PlayerState.falling)
        {
            if (this.playerCapsuleCollider.IsTouchingLayers(groundLayer))
            {
                state = PlayerState.idle;
            }
        }

        else if (Mathf.Abs(rigidBody.velocity.x) > minRunningStateVelocity_X)
        {
            state = PlayerState.running;
        }

        else
        {
            state = PlayerState.idle;
        }

        if (rigidBody.velocity.y < minFreeFallStateVelocity_Y)
        {
            state = PlayerState.falling;
        }

        if (state == PlayerState.attack)
        {

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
