using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;
	// The time in seconds between each attack.
	public int attackDamage = 10;
	// The amount of health taken away per attack.


	//	Animator anim;                              // Reference to the animator component.
	GameObject player;
	// Reference to the player GameObject.
	PlayerHealth playerHealth;
	// Reference to the player's health.
	EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	bool playerInRange;
	// Whether player is within the trigger collider and can be attacked.
//	DoorController c_doorController;
	bool doorInRange;
	float timer;
	// Timer for counting up to the next attack.


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
//		anim = GetComponent <Animator> ();
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = true;
		}
		//if (other.gameObject.CompareTag ("Door")) {
		//	doorInRange = true;
		//	c_doorController = other.gameObject.GetComponentInParent<DoorController>();
		//}
	}


	void OnTriggerExit (Collider other)
	{
		if (other.gameObject == player) {
			playerInRange = false;
		}
		//if (other.gameObject.CompareTag ("Door")) {
		//	doorInRange = false;
		//}
	}


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) { 
				AttackPlayer ();
		}

		//if (timer >= timeBetweenAttacks && doorInRange) {
		//	AttackDoor ();
		//}
			

		//if(playerHealth.currentHealth <= 0)
		//{
		//	anim.SetTrigger ("PlayerDead");
		//}
	}


	void AttackPlayer ()
	{
		timer = 0f;

		if (playerHealth.currentHealth > 0) {
			playerHealth.TakeDamage (attackDamage);
		}
	}


//	void AttackDoor ()
//	{
//		timer = 0f;
//		c_doorController.TakeDamage (attackDamage);

//	}

}
