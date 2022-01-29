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
    private List<GameObject> players;
    private int currentPlayerIndex;

    #region HealthBar
    private bool healthBarIsActive;
    private bool healthBarUIFound;
    protected Slider healthSlider;
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

        players = new List<GameObject>();
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        currentPlayerIndex = 1;
        for (int i = 1; i < players.Count; i++)
        {
            players[i].SetActive(false);
            Debug.Log(players[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lifePoints);
        Timer += Time.deltaTime;
        if (Timer > 1)
        {
            Timer = 0;
            if (currentHealth >= 10 && currentHealth <=15)
            {
                lifePoints++;
            }
            else
            {
                lifePoints = 0;
            }
        }

        if (healthBarUIFound == false && GameObject.FindGameObjectWithTag("HealthBar") != null)
        {
            healthSlider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
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
        if (lifePoints > 5)
        {
            this.state = PlayerState.transitionBody;
        }
    }

    public void PlayerTransition()
    {
        base.sprite.enabled = false;
        players[currentPlayerIndex + 1].SetActive(true);
        //players[currentPlayerIndex].SetActive(false);
    }
}
