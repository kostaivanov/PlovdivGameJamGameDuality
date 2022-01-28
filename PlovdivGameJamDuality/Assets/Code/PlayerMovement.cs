using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : ObjectComponents, IMovable
{
    private float moveAxis_X, moveAxis_Y;
    private float moveDirection;
    [SerializeField] private float speed;
    private bool isMoving;
    private float extraDistance = 0.1f;

    [SerializeField] private float jumpForce;
    [SerializeField] private float decayRate;

    private float jumpForce_2;

    private float extrHeightText = 0.1f;

    private bool jumpPressed;
    private bool jumpHolded;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isMoving = false;
        jumpPressed = false;

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
            isMoving = false;
            moveAxis_X = 0;
            moveAxis_Y = 0f;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {

            //jumpButton.jumpButtonClicked = false;

            jumpPressed = true;

        }

    }


    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            Move();
        }

        if (jumpPressed == true && CheckIfIsGrounded() == true)
        {
            StartCoroutine(JumpProcess());

            jumpPressed = false;
        }
        if (!CheckIfIsGrounded())
        {
            jumpPressed = false;
        }
    }

    private void ProcessInput()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        moveAxis_Y = Input.GetAxisRaw("Vertical");
        //moveDirection = new Vector2(moveAxis_X, moveAxis_Y).normalized;
        isMoving = true;
    }

    public void Move()
    {
        if (moveDirection < 0)
        {
            this.transform.localScale = new Vector2(-1, 1);
            rigidBody.velocity = new Vector2(-speed * Time.fixedDeltaTime, rigidBody.velocity.y);
        }
        if (moveDirection > 0)
        {
            this.transform.localScale = new Vector2(1, 1);
            rigidBody.velocity = new Vector2(speed * Time.fixedDeltaTime, rigidBody.velocity.y);
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

    private IEnumerator JumpProcess()
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
}
