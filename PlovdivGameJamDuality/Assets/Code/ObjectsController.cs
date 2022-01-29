using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour, IMovable
{
    [SerializeField] float speed;
    //[SerializeField] private MeteoriteHealthController healthController;
    private Rigidbody2D rigidBody;
    internal Vector2 startPosition;
    private bool move = false;

    private void OnEnable()
    {
        move = true;

        Invoke("Disable", 10f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;

        rigidBody = GetComponent<Rigidbody2D>();
    }

    internal void Disable()
    {
        this.gameObject.SetActive(false);
        move = false;
    }

    private void FixedUpdate()
    {
        if (move == true)
        {
            Move();
        }
    }

    public void Move()
    {
        if (rigidBody != null)
        {
            rigidBody.velocity = Vector2.left * speed * Time.fixedDeltaTime;
        }
    }
}
