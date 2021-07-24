using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWave : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float WaveLength = 5f;
    public float WaveSpeed = 5f;
    public float Amplitude = 5f;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawTravellingSineWave(Vector3.zero, Amplitude, WaveLength, WaveSpeed);
    }

    void DrawTravellingSineWave(Vector3 startPoint, float amplitude, float wavelength, float waveSpeed)
    {

        float x = 0f;
        float y;
        float k = 2 * Mathf.PI / wavelength;
        float w = k * waveSpeed;
        lineRenderer.positionCount = 200;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            x += i * 0.001f;
            y = amplitude * Mathf.Sin(k * x + w * Time.time);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + startPoint);
        }
    }
}
