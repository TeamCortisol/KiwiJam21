using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    public float timeBetweenSpawn;
    private float spawnTime;
    
    // Start is called before the first frame update
    void Update()
    {
        if (Time.time > spawnTime)
        {
            SpawnTile();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    private void SpawnTile()
    {
        Instantiate(tile, transform.position, Quaternion.identity);
        // need to have random resize
    }
}
