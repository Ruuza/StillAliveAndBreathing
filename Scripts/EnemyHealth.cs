using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public int MoneyValue = 10;                 // The amount added to the player's score when the enemy dies.
	//public AudioClip deathClip;                 // The sound to play when the enemy dies.

	public GameObject bloodPrefab;
    CapsuleCollider capsuleCollider;

    Animator anim;

	//AIPath cAIPath;
	WaveSpawner cWaweSpawner;

//	AudioSource enemyAudio;                     // Reference to the audio source.


	void Awake ()
	{
//		enemyAudio = GetComponent <AudioSource> ();
//		cAIPath = gameObject.GetComponent<AIPath>();
		currentHealth = startingHealth;
		cWaweSpawner = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<WaveSpawner>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
	}

	void Update ()
	{


	}


	public void TakeDamage (int amount)
	{
        
        //		enemyAudio.Play ();
        //cAIPath.speed = cAIPath.GetStartSpeed() / 2;
        Vector3 bloodPosition = transform.position;
        bloodPosition.y = 0.1f;
		Instantiate (bloodPrefab, bloodPosition, Quaternion.Euler(90f, Random.Range(0, 360), 0f));
		currentHealth -= amount;

		if(currentHealth <= 0)
		{
			Death ();
		}
	}


	void Death ()
	{
        
            // Turn the collider into a trigger so shots can pass through it.
            capsuleCollider.isTrigger = true;

            // Tell the animator that the enemy is dead.
            anim.SetTrigger("Dead");

            // Find and disable the Nav Mesh Agent.
            //GetComponent <NavMeshAgent> ().enabled = false;

            MoneyManager.money += MoneyValue;
		//Instantiate (bloodPrefab, transform.position, transform.rotation);

		cWaweSpawner.DecreaseAliveEnemies();

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        //enemyAudio.clip = deathClip;

        //Destroy (gameObject);
        Destroy(gameObject, 10f);
    }
		
}