using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public float timeDecrease;
    public float minTime;

    public GameObject[] obstacles;

    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
    }

    private void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            int rand = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[rand], transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
            if (startTimeBtwSpawns > minTime)
            {
                startTimeBtwSpawns -= timeDecrease;
            }
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
