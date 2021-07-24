using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] float SpawnDelay = 3f;
    [SerializeField] GameObject Meteor;

    private ScreenGameplaySettings _screenGameplayMod;

    // Start is called before the first frame update
    void Start()
    {
        _screenGameplayMod = GetComponentInParent<ScreenGameplaySettings>();
        StartCoroutine(SpawnMeteor(SpawnDelay * _screenGameplayMod.InitialSpeed));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SpawnMeteor(float delay)
    {
        while (true)
        {
            var nextSpawnTime = delay - Mathf.Min(delay - 0.1f, _screenGameplayMod.CurrentSpeed);
            yield return new WaitForSeconds(nextSpawnTime);
            var horizontalSpawnDistance = Random.Range(-5, 5);
            Instantiate(Meteor, new Vector3(transform.position.x + horizontalSpawnDistance, transform.position.y), Quaternion.identity);
        }
    }
}
