using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    [SerializeField] public float GlobalSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GlobalSpeed += 0.02f * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GlobalSpeed += 0.1f;
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow) && GlobalSpeed > 0.01f)
        {
            GlobalSpeed -= 0.1f;
        }
    }
}
