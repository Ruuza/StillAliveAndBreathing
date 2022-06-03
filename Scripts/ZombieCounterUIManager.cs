using UnityEngine;
using UnityEngine.UI;

public class ZombieCounterUIManager : MonoBehaviour {


	Text text;
	WaveSpawner cWaveSpawner;

	void Start () {
		text = GetComponent<Text> ();
		cWaveSpawner = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<WaveSpawner> ();
	}

	void Update () {
		if (cWaveSpawner.GetAliveEnemies() > 0)
			text.text = "Alive: " + cWaveSpawner.GetAliveEnemies();
		else
			text.text = "Next wave in: " + cWaveSpawner.GetWaveCountDownRounded ();
	}
}
