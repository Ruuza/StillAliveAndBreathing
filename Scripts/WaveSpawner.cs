using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour {

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	public Transform enemy1;
    public Transform enemy2;
    public int zombiesStartCount;
	public float rate;


	public Transform[] spawnPoints;
	public int zombiesPerDay = 4;

	public float timeBetweenWaves = 5f;
	private float waveCountdown;

	private int aliveEnemies = 0;

	private SpawnState state = SpawnState.COUNTING;

    AudioSource spawnerAudio;
    public AudioClip waveStart;
    public AudioClip waveEnd;
    public AudioClip zombieDie;
    public AudioClip zombieSpawn;
    public AudioClip monsterSpawn;

    public static int day = 1;

	void Start()
	{

        spawnerAudio = GetComponent<AudioSource>();

		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}

		waveCountdown = 5;
	}

	void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (aliveEnemies <= 0)
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine( SpawnWave () );
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
        spawnerAudio.clip = waveEnd;
        spawnerAudio.Play();

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;
		day++;

	}


	IEnumerator SpawnWave()
	{

        spawnerAudio.clip = waveStart;
        spawnerAudio.Play();

		state = SpawnState.SPAWNING;
		aliveEnemies = zombiesStartCount + day*zombiesPerDay;

		for (int i = 0; i < zombiesStartCount + day*zombiesPerDay; i++)
		{
            if (i % 10 == 0 && i != 0)
            {

                spawnerAudio.clip = monsterSpawn;
                spawnerAudio.Play();

                SpawnEnemy(enemy2);
            }
            else {

                spawnerAudio.clip = zombieSpawn;
                spawnerAudio.Play();

                SpawnEnemy(enemy1);
            }
			yield return new WaitForSeconds( 1f/( rate*( (float)day * zombiesPerDay/4 )) );
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform _enemy)
	{
		Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length) ];
		Instantiate(_enemy, _sp.position, _sp.rotation);
	}

	public int GetAliveEnemies(){
		return aliveEnemies;
	}

	public void DecreaseAliveEnemies(){
        spawnerAudio.clip = zombieDie;
        spawnerAudio.Play();

		aliveEnemies--;
	}

	public int GetWaveCountDownRounded(){
		return (int)waveCountdown;
	}

	public int GetDay(){
		return day;
	}
}
