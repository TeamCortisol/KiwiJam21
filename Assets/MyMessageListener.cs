using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MyMessageListener : MonoBehaviour {
    public SerialController serialController;
    public TextMeshProUGUI txt;

    public Window_Graph windowGraph;

    public int beatsLogSize = 500;
    private GlobalState globalState;

    private List<int> buffer = new List<int>();

    private float threshold = 620.0F;  //Threshold at which BPM calculation occurs
    private bool belowThreshold = true;
    private long beat_old = 0;
    private List<float> beats = new List<float>();
    private int beatIndex = 0;
    private int BPM = 0;





    // Use this for initialization
    void Start () {
            serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
            globalState = FindObjectOfType<GlobalState>();

        EventManager.Subscribe(GameEvent.TopLeftDeath, _ => 
        {
            Zap();
        });
        EventManager.Subscribe(GameEvent.TopRightDeath, _ => 
        {
            Zap();
        });
        EventManager.Subscribe(GameEvent.BottomLeftDeath, _ => 
        {
            Zap();
        });
        EventManager.Subscribe(GameEvent.BottomRightDeath, _ => 
        {
            Zap();
        });
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
        if (msg == "!" || msg == "!\r") {

        }
        else {
            
            int voltage = Int32.Parse(msg);
            txt.text = BPM + " BPM";
            // Target Heart Rate (HR) Zone (60-85%): 117 – 166
            float diff = ((float) BPM - 80.0F) / (166.0F - 117.0F);
            float diffClamped = Mathf.Clamp(diff, 0.0F, 1.0F);
            if (globalState) {
                globalState.Difficulty = diffClamped;
            }

            buffer.Add(voltage);
            if (buffer.Count > 66) {
                buffer.RemoveAt(0);
            }


            // BPM calculation check
            if (voltage > threshold && belowThreshold == true)
            {
                calculateBPM();
                belowThreshold = false;
            }
            else if(voltage < threshold)
            {
                belowThreshold = true;
            }


            windowGraph.renderBpms(buffer);
        }

    }

    void calculateBPM () 
    {  
        long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        long beat_new = milliseconds;    // get the current millisecond
        long diff = beat_new - beat_old;    // find the time between the last two beats
        
        float currentBPM = 60000 / diff;    // convert to beats per minute
        // beats[beatIndex] = currentBPM;  // store to array to convert the average
        beats.Add(currentBPM);
        if (beats.Count > beatsLogSize) {
            beats.RemoveAt(0);
        }
        float total = 0.0F;
        for (int i = 0; i < beats.Count; i++){
            total += beats[i];
        }
        BPM = (int)(total / beats.Count);
        beat_old = beat_new;
        // beatIndex = (beatIndex + 1) % beatsLogSize;  // cycle through the array instead of using FIFO queue
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }

    void Zap() {
        serialController.SendSerialMessage("zap\r");
    }
    void Zaap() {
        serialController.SendSerialMessage("zaap\r");
    }
    void Zaaap() {
        serialController.SendSerialMessage("zaaap\r");
    }
}