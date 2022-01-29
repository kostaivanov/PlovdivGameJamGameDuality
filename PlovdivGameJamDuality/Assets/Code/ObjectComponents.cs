using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal abstract class ObjectComponents : MonoBehaviour
{

    #region Unity Components
    protected Rigidbody2D rigidBody;
    protected Collider2D collider2D;
    protected Animator animator;
    protected SpriteRenderer sprite;
    internal LayerMask groundLayer;
    #endregion


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("GroundLayer");

    }
}
