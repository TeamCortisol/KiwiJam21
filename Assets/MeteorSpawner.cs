using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] float SpawnDelay = 3f;
    [SerializeField] GameObject Meteor;

    private GlobalState _globalState;

    // Start is called before the first frame update
    void Start()
    {
        _globalState = FindObjectOfType<GlobalState>();
        StartCoroutine(SpawnMeteor(SpawnDelay));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SpawnMeteor(float delay)
    {
        while (true)
        {
            var nextSpawnTime = delay - Mathf.Min(delay - 0.1f, _globalState.GlobalSpeed);
            yield return new WaitForSeconds(nextSpawnTime);
            var horizontalSpawnDistance = Random.Range(-5, 5);
            Instantiate(Meteor, new Vector3(transform.position.x + horizontalSpawnDistance, transform.position.y), Quaternion.identity);
        }
    }
}
