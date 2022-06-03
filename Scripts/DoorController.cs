using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public GameObject door;
	public float doorClosingSpeed = 0.1f;

	private float doorMaxHealth;
	private float doorMinHealth;
	private float minMaxDifference;


	void Start () {
		doorMinHealth = door.transform.position.x;
		doorMaxHealth = door.transform.position.x - door.GetComponent<SpriteRenderer> ().bounds.size.x;
		minMaxDifference = doorMaxHealth - doorMinHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space) ) {
			if (door.transform.position.x <= doorMaxHealth)
				door.transform.position = new Vector3(doorMaxHealth, door.transform.position.y, door.transform.position.z);
			else
			door.transform.position += new Vector3(minMaxDifference * doorClosingSpeed * Time.deltaTime,0,0);
		}
	}

	public void TakeDamage(int damage){
		door.transform.position -= new Vector3 (damage * minMaxDifference/1000,0,0);
	}


}
