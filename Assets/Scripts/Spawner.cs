using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnPrefabs;
    public float minDelay = 3.0f;
    public float maxDelay = 5.0f;
    public float startDelay = 3.0f;

    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(startDelay);

        Debug.Log("Ready to spawn Obstacles...");
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            int prefab_idx = Random.Range(0, spawnPrefabs.Length);


            // Spawn object
            //AudioManager.Instance.Play("Spawn");
            Instantiate(spawnPrefabs[prefab_idx], transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (_timer <= 0)
    //    {
    //        Instantiate(obstacle, transform.position, Quaternion.identity);
    //        _timer = spawnTime;
    //    }
    //    else {
    //        _timer -= Time.deltaTime;
    //    }
    //}
}
