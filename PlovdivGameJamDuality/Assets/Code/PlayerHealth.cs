using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
internal class PlayerHealth : ObjectComponents
{
    private int lowerBoundLife = 10;
    private int upperBoundLife = 15;
    private int lifePoints = 0;
    private float Timer;
    private const float decreasePerMinute = 5;
    internal PlayerState state;
    [SerializeField] private List<GameObject> players;
    private int currentPlayerIndex;

    #region HealthBar
    private bool healthBarIsActive;
    private bool healthBarUIFound;
    private GameObject healthBar;
    protected Slider healthSlider;

    private int previousIndexPlayer;
    private bool TransitionCompleted;
    #endregion

    #region Player
    [SerializeField] protected float fullHealth;
    internal float currentHealth;


    private bool isAlive;
    internal bool fellOffWorld;
    #endregion

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        healthBarIsActive = false;
        fellOffWorld = false;

        currentHealth = fullHealth;
        healthBarUIFound = false;
        //Debug.Log(lifePoints);

        //players = new List<GameObject>();
        //players = GameObject.FindGameObjectsWithTag("Player").ToList();
        if (this.gameObject.name == "1_Victor")
        {
            currentPlayerIndex = 0;
        }
        if (this.gameObject.name == "2_Victor")
        {
            currentPlayerIndex = 1;
        }
        if (this.gameObject.name == "3_Victor")
        {
            currentPlayerIndex = 2;
        }


        previousIndexPlayer = currentPlayerIndex;
        for (int i = 1; i < players.Count; i++)
        {
            players[i].SetActive(false);
        }
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        TransitionCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(players[currentPlayerIndex].name);
        Timer += Time.deltaTime;
        //Debug.Log("TIme = " + Timer + " - life points = " + lifePoints);
        Debug.Log("life points = " + lifePoints);
        Debug.Log("index player  = " + currentPlayerIndex);


        if (Timer > 1)
        {
            Timer = 0;
            if (currentHealth > 10 && currentHealth < 15)
            {
                lifePoints++;
            }
            else
            {
                lifePoints = 0;
            }
        }

        if (healthBarUIFound == false && healthBar != null)
        {
            healthSlider = healthBar.GetComponent<Slider>();
            healthBarIsActive = true;
            healthBarUIFound = true;
            healthSlider.maxValue = fullHealth;
            healthSlider.value = fullHealth;
        }

        if (healthBarIsActive == true && this.gameObject.transform.position.y < 0 && currentHealth > 0)
        {
            currentHealth -= Time.deltaTime * decreasePerMinute / 5f;
            this.healthSlider.value = this.currentHealth;
        }
        else if(healthBarIsActive == true && this.gameObject.transform.position.y > 0 && currentHealth < 25)
        {
            currentHealth += Time.deltaTime * decreasePerMinute / 5f;
            this.healthSlider.value = this.currentHealth;
        }

        if (this.currentHealth <= 0 && this.isAlive == true)
        {
            //base.playerMovement.enabled = false;
            //if (fellOffWorld != true)
            //{
            //    playerAudioSource.PlayOneShot(dyingSound);
            //}

            //this.playerAnimator.SetTrigger("isDead");
            isAlive = false;
        }
    }

    private void LateUpdate()
    {
        this.AnimationStateSwitch();
        base.animator.SetInteger("state", (int)state);


    }

    protected void AnimationStateSwitch()
    {
        if (lifePoints > 3 && TransitionCompleted == false)
        {
            this.state = PlayerState.transitionBody;
        }
    }

    public void PlayerTransition()
    {
        base.sprite.enabled = false;
        base.collider2D.enabled = false;
        //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //this.gameObject.GetComponent<Collider2D>().enabled = false;

        this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;

        //Debug.Log("transition");
        if (this.gameObject.name == "1_Victor")
        {
            players[1].SetActive(true);
            Debug.Log("1");
        }
        else if (this.gameObject.name == "2_Victor")
        {
            players[2].SetActive(true);
            Debug.Log("2");
        }
        else if (this.gameObject.name == "3_Victor")
        {
            players[3].SetActive(true);
            Debug.Log("3");
        }


        //this.state = PlayerState.running;
        //currentPlayerIndex++;
        //StartCoroutine(Deactive());
    }

    private IEnumerator Deactive()
    {
        yield return new WaitForSecondsRealtime(3);
        //this.gameObject.SetActive(false);
        //if (players[0].activeSelf == true)
        //{
        //    Debug.Log("asd1");

        //}
        //else if (players[1].activeSelf == true)
        //{
        //    Debug.Log("asd2");
        //    players[1].SetActive(false);
        //}
        //else if (players[2].activeSelf == true)
        //{
        //    Debug.Log("asd3");
        //    players[2].SetActive(false);
        //}
    }
}
