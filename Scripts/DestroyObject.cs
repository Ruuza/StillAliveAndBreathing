using UnityEngine;

public class DestroyObject : MonoBehaviour {

	public float timeToDestroy = 30f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject, timeToDestroy);
	}
}
