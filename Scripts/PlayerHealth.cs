using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public float flashSpeed = 3f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.2f);     // The colour the damageImage is set to, to flash.
    public Animator gameOverAnimator;
//	public GameObject blood;

//	BloodSpawner bloodSpawner;

//	Animator anim;                                              // Reference to the Animator component.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerController PlayerController;                              // Reference to the PlayerShooting script.
    public GameObject weapons;
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.


    float restartTimer;
    float restartDelay = 8f;

	void Awake ()
	{
//		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		PlayerController = GetComponentInChildren <PlayerController> ();


		currentHealth = startingHealth;
	}


	void Update ()
	{
		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
			
		damaged = false;

        if (isDead) {

            restartTimer += Time.deltaTime;
            if (restartTimer > restartDelay) {
                WaveSpawner.day = 1;
                MoneyManager.money = 0;

                Application.LoadLevel(Application.loadedLevel);
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("QUIT");
                Application.Quit();
            }

        }


	}


	public void TakeDamage (int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;

//		playerAudio.Play ();

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}


	void Death ()
	{
		isDead = true;

		// Turn off any remaining shooting effects.
//		playerShooting.DisableEffects ();

//		anim.SetTrigger ("Die");

		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		playerAudio.clip = deathClip;
		playerAudio.Play ();


        //		bloodSpawner.SpawnBlood (playerTransform);
        PlayerController.enabled = false;
        weapons.SetActive(false);


        gameOverAnimator.SetTrigger("GameOver");


      



    }

    public void HealPlayer() {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

}
