using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    //public ParticleSystem spawnEffectPrefab;

    public float timeBetweenWaves = 5f;
    private float countDown = 5f;

    private int waveNumber = 1;

    private int maxWaveNumber = 2;

    void Start()
    {

    }

    void Update()
    {
        if (countDown <= 0f && waveNumber <= maxWaveNumber)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.7f);
        }
        waveNumber++;
    }

    void SpawnEnemy()
    {
        Vector3 position = new Vector3(spawnPoint.position.x + Random.Range(-10.0f, 10.0f), 0f, spawnPoint.position.z + Random.Range(-10.0f, 10.0f));
        //Instantiate(spawnEffectPrefab, position, Quaternion.Euler(-90, 0, 0));
        Instantiate(enemyPrefab, position, Quaternion.Euler(0, Random.Range(-180.0f, 180.0f), 0));
        
    }
}
