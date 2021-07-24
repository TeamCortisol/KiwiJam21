using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private Material myMaterial;
    private bool isWhite;
    private bool playerTouchedTile;

    void Start()
    {
        isWhite = true;
    }

    private void SwitchColour()
    {
        if (isWhite)
        {
            myMaterial.color = Color.black;
            isWhite = false;
        }
        else
        {
            myMaterial.color = Color.white;
            isWhite = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.L) && playerTouchedTile)
        {
            SwitchColour();
        }
        else
        {
            Debug.Log("died");
        }
    }

    private void OnTriggerEnter2D(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerTouchedTile = true;
        }
    }

    private void OnTriggerExit2D(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerTouchedTile = false;
        }
    }

    // private void OnTriggerEnter(Collider collider)
    // {
    //     if (collider.CompareTag("Player"))
    //     {
    //         
    //         if (myMaterial.color == Color.white)
    //         {
    //             myMaterial.color = Color.black;
    //             isWhite = false;
    //         }
    //         else
    //         {
    //             myMaterial.color = Color.white;
    //         }
    //     }
    // }
}
