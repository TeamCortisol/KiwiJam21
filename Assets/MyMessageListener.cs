using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MyMessageListener : MonoBehaviour {
    public SerialController serialController;
    public TextMeshProUGUI txt;
    private GlobalState globalState;

    // Use this for initialization
    void Start () {
            serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
            globalState = FindObjectOfType<GlobalState>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Vertical") > 0.0F) {
            Debug.Log("Sending zaaap");
            serialController.SendSerialMessage("zaaap\r");
        }
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        // Debug.Log("Arrived: " + msg);
        // Debug.Log("Arrived: " + "asd");
        int bpm = Int32.Parse(msg);
        txt.text = bpm + " BPM";
        // Target Heart Rate (HR) Zone (60-85%): 117 – 166
        float diff = ((float) bpm - 117.0F) / (166.0F - 117.0F);
        float diffClamped = Mathf.Clamp(diff, 0.0F, 1.0F);
        globalState.Difficulty = diffClamped;
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}