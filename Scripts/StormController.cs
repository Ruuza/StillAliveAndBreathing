using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormController : MonoBehaviour {

	public int stormDelay = 10;
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		StartCoroutine (Storm(stormDelay));
	}
	
	IEnumerator Storm(int delay){
		while (true) {
			yield return new WaitForSeconds(delay);

			spriteRenderer.color = new Color(128,128,128);

			yield return new WaitForSeconds (0.5f);

			spriteRenderer.color = new Color (0,0,0);

		
		}


	}

}
