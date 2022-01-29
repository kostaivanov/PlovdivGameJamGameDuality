using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private const float decreasePerMinute = 5;

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
    void Start()
    {
        healthBarIsActive = false;
        fellOffWorld = false;

        currentHealth = fullHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (healthBarUIFound == false && GameObject.FindGameObjectWithTag("HealthBar") != null)
        {
            healthSlider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
            healthBarIsActive = true;

            healthSlider.maxValue = fullHealth;
            healthSlider.value = fullHealth;
        }

        if (healthBarIsActive == true && this.gameObject.transform.position.y < 0 && currentHealth > 0)
        {
            Debug.Log(currentHealth);
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
}
