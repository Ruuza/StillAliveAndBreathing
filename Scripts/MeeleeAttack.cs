using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleeAttack : MonoBehaviour
{

	[SerializeField]
	Animator anim;

	public int damage = 20;
	public float timeBetweenAttacks = 1f;
	public int animWeaponType;
	public float delayToDamage = 0.4f;

	List<GameObject>nearEnemy = new List<GameObject>();

	float timer;

	void Start ()
	{
		
	}

	void OnEnable ()
	{
		anim.SetInteger ("WeaponType", animWeaponType);
		anim.SetTrigger ("WeaponChange");

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Enemy" && !col.isTrigger)
			nearEnemy.Add(col.gameObject);
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "Enemy" && !col.isTrigger)
			nearEnemy.Remove(col.gameObject);
	}


	void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && Input.GetButtonDown ("Fire1")) {
			StartCoroutine (Attack());
		}
	}


	IEnumerator Attack ()
	{

		timer = 0f;
		anim.SetTrigger ("Attack");

		Debug.Log ("Utocim");

		yield return new WaitForSeconds (delayToDamage);

		for (int i = 0; i < nearEnemy.Count; i++) {
			EnemyHealth enemyHealth = nearEnemy[i].GetComponent <EnemyHealth> ();
			if (enemyHealth != null) {
				enemyHealth.TakeDamage (damage);
			}

		}

	}


}
