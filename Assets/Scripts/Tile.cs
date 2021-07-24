using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int moveSpeed;
    [SerializeField] private Material myMaterial;
    private bool isWhite;
    private bool playerTouchedTile;

    void Start()
    {
        isWhite = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
        if (Input.GetKeyUp(KeyCode.L) && playerTouchedTile)
        {
            SwitchColour();
            Debug.Log("colour switch");
        }
        else
        {
            // Debug.Log("died");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Debug.Log("hit wall");
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            playerTouchedTile = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerTouchedTile = false;
        }
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

}