using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGame : MonoBehaviour
{
    // for display in debugger
    [SerializeField] float SurvivedTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SurvivedTime += Time.deltaTime;
    }
}
