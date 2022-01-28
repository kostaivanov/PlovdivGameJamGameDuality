using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    //[SerializeField] float meteoriteSpeed;
    //[SerializeField] private MeteoriteHealthController healthController;
    private Rigidbody2D rigidBody;
    internal Vector2 startPosition;
    private bool move = false;

    private void OnEnable()
    {
        move = true;

        //if (healthController != null)
        //{
        //    if (this.gameObject.tag == "SmallMeteorite")
        //    {
        //        healthController.health = healthController.fullHealthSmallMeteorite;
        //    }
        //    else if (this.gameObject.tag == "MediumMeteorite")
        //    {
        //        healthController.health = healthController.fullHealthMediumMeteorite;
        //    }
        //    else if (this.gameObject.tag == "BigMeteorite")
        //    {
        //        healthController.health = healthController.fullHealthBigMeteorite;
        //    }

        //}

        Invoke("Disable", 3f);
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
        //rigidBody.velocity = Vector2.left * PermanentFunctions.instance.meteoriteSpeed;
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
            if (rigidBody != null)
            {
                rigidBody.velocity = Vector2.left * 2f;
            }
        }
    }
}
