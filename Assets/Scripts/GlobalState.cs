using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] public float Difficulty = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && Difficulty < 1f)
        {
            Difficulty += 0.1f;
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow) && Difficulty > 0.01f)
        {
            Difficulty -= 0.1f;
        }
    }
}
