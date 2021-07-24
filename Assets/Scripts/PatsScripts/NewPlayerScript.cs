using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerScript : MonoBehaviour
{
    [SerializeField] float speed;
    private float touchStartTime;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            touchStartTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            float delta = Time.time - touchStartTime;
            Debug.Log(delta.ToString());
            float adjustedSpeed = speed * delta;
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * adjustedSpeed);
        }
    }

    // void Update ()
    // {
    //     if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(Action());
    //     if (Input.GetKeyUp(KeyCode.E)) StopCoroutine(Action());
    // }
    //
    // IEnumerator Action ()
    // {
    //     yield return new WaitForSeconds(time);
    //
    //     // do stuff
    // }
}
