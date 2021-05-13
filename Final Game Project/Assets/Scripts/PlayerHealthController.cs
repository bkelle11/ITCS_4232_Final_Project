using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;
    public float invincFrames;
    private float invincCount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = CharacterTracker.instance.curHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCount > 0)
        {
            invincCount -= Time.deltaTime;
            if(invincCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, // Red color value
                                                                   PlayerController.instance.bodySR.color.g, // Green color value
                                                                   PlayerController.instance.bodySR.color.b, // Blue color value
                                                                   1f);                                      // Alpha value
            }
        }
    }

    public void DamagePlayer()
    {
        if (invincCount <= 0)
        {
            currentHealth--;
            invincCount = invincFrames;
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, // Red color value
                                                               PlayerController.instance.bodySR.color.g, // Green color value
                                                               PlayerController.instance.bodySR.color.b, // Blue color value
                                                               0.5f);                                    // Alpha value

            if (currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX(2);

                PlayerController.instance.gameObject.SetActive(false);

                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void MakeInvincible(float time)
    {
        invincCount = time;
        PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, // Red color value
                                                           PlayerController.instance.bodySR.color.g, // Green color value
                                                           PlayerController.instance.bodySR.color.b, // Blue color value
                                                           0.5f);                                    // Alpha value
    }

    public void HealPlayer(int amount)
    {
        AudioManager.instance.PlaySFX(4);
        
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
