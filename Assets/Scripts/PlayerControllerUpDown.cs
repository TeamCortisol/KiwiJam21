using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUpDown : MonoBehaviour
{
    private Vector2 targetPos;
    public float yStep = 5f;
    public float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPos = new Vector2(transform.position.x, transform.position.y + yStep);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPos = new Vector2(transform.position.x, transform.position.y - yStep);
        }
    }
}
