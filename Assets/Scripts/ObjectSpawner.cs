using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;
    [SerializeField] private float timeCounter;

    [System.Serializable]
    public class Wave
    {
        public ObjectPooler pool;
        public float spawnTimer;
        public float spawnInterval;
        public int objectsPerwave;
        public int spawnedObjectCount;
    }

    void Update()
    {
        waves[waveNumber].spawnTimer += Time.deltaTime * GameManager.Instance.worldSpeed;
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
        {
            waves[waveNumber].spawnTimer = 0;
            SpawnObject();
        }
        if (waves[waveNumber].spawnedObjectCount >= waves[waveNumber].objectsPerwave)
        {
            waves[waveNumber].spawnedObjectCount = 0;
            waveNumber++;
            if(waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }

        timeCounter += Time.deltaTime;
        if (timeCounter >= 10f)
        {
            timeCounter = 0f;
            foreach (Wave wave in waves)
            {
                if (wave.spawnInterval > 1f)
                {
                    wave.spawnInterval = Mathf.Max(wave.spawnInterval - 1f, 1);
                }
            }
        }
    }

    private void SpawnObject()
    {
        GameObject spawnedObject = waves[waveNumber].pool.GetPooledObject();
        spawnedObject.transform.position = RandomSpawnPoint();
        spawnedObject.transform.rotation = transform.rotation;
        spawnedObject.SetActive(true);
        waves[waveNumber].spawnedObjectCount++;
    }

    // this method will call two child object of Objectspawn then set first spawn point
    // from y(vertical) and random object position x(horizontal)
    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;
        spawnPoint.x = Random.Range(minPos.position.x,maxPos.position.y);
        spawnPoint.y = minPos.position.y;

        return spawnPoint;
    }
}
