using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrow : MonoBehaviour
{
    public float growSpeed = 0.5f;
    public float threshold = 10f;
    private Vector3 originalScale;
    
    private void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        
        transform.localScale *= 1 + growSpeed * Time.deltaTime;

        if (transform.localScale.y > threshold * originalScale.y)
        {
            transform.localScale = originalScale;
        }
    }

    
}
