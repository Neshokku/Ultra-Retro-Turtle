using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject instance;
    public bool spawnOnFail;

    [SerializeField] private float firstSpawn;
    public float minSpawnRate;
    public float maxSpawnRate;

    private float nextSpawn;

    private float timer = 0.0f;
    public float spawnOffset = 4;

    private List<Vector3> recentSpawns = new List<Vector3>();

    public float minSpawnDistance;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = firstSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < nextSpawn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Spawn();
            nextSpawn = Random.Range(minSpawnRate, maxSpawnRate);
            timer = 0.0f;
        }
    }

    bool TooCloseToRecentSpawns(Vector3 posToCheck)
    {
        foreach (var recentPos in recentSpawns) 
        {
            if (Vector3.Distance(recentPos, posToCheck) < minSpawnDistance)
            {
                return true;
            }
        }
        return false;
    }

    void Spawn()
    {
        float leftPoint = transform.position.x - spawnOffset;
        float rightPoint = transform.position.x + spawnOffset;

        int maxAttemptsToSpawn = 10;

        int spawnMemoryCapacity = 2;

        Vector3 spawnPosition = Vector3.zero;
        bool validPosition = false;

        for (int i = 0; i < maxAttemptsToSpawn; i++)
        {
            spawnPosition = new Vector3(Random.Range(leftPoint, rightPoint), transform.position.y, transform.position.z);

            if (!TooCloseToRecentSpawns(spawnPosition))
            {
                validPosition = true;
                break;
            }
        }

        if (validPosition)
        {
            Instantiate(instance, spawnPosition, transform.rotation);
            recentSpawns.Add(spawnPosition);
        }
        else
        {
            if (spawnOnFail)
            {
                Instantiate(instance, spawnPosition, transform.rotation);
            }
        }

        if (recentSpawns.Count > spawnMemoryCapacity)
        {
            recentSpawns.RemoveAt(0);
        }
    }
}
