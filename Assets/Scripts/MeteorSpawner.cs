using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] float MinimumSpawnDelay = 0.1f;
    [SerializeField] float AverageInitialSpawnDelay = 10f;
    [SerializeField] float RandomPos = 5f;
    [SerializeField] GameObject[] Meteors;

    private Screen _screenGameplayMod;

    // Start is called before the first frame update
    void Start()
    {
        _screenGameplayMod = GetComponentInParent<Screen>();
        var initialSpawnDelay = Random.Range(0.7f * AverageInitialSpawnDelay, 1.3f * AverageInitialSpawnDelay);
        StartCoroutine(SpawnMeteor(initialSpawnDelay));
    }

    private IEnumerator SpawnMeteor(float delay)
    {
        while (true)
        {
            var nextSpawnTime = Mathf.Max(MinimumSpawnDelay, delay * (1 - _screenGameplayMod.CurrentDifficulty));
            yield return new WaitForSeconds(nextSpawnTime);
            
            var prefabIdx = Random.Range(0, Meteors.Length);
            var meteor = Instantiate(Meteors[prefabIdx], GetRandomSpawnLocation(), Quaternion.identity);
            meteor.transform.parent = transform;
        }
    }

    private Vector3 GetRandomSpawnLocation()
    {
        // Spawn somewhere along the top right corner of game
        // Decide to spawn along top edge or right edge first
        var generateOnTop = Random.value > 0.5f;
        var distance = Random.Range(-RandomPos, 0);
        if (generateOnTop)
        {
            return new Vector3(transform.position.x + distance, transform.position.y);
        }
        else // if generateOnRight
        {
            return new Vector3(transform.position.x, transform.position.y + distance);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(
            new Vector3(transform.position.x - RandomPos, transform.position.y, transform.position.z), 
            new Vector3(transform.position.x, transform.position.y, transform.position.z)
        );
        Gizmos.DrawLine(
            new Vector3(transform.position.x, transform.position.y - RandomPos, transform.position.z), 
            new Vector3(transform.position.x, transform.position.y, transform.position.z)
        );
    }
}
