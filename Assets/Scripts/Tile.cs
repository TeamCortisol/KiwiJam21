using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int moveSpeed;
    private bool _playerTouchedTile;
    private Screen _screenGameplayMod;

    void Start()
    {
        _screenGameplayMod = GetComponentInParent<Screen>();
        _playerTouchedTile = false;
    }
    
    void Update()
    {
        transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerTouchedTile = true;
            SwitchColour();
        }
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void SwitchColour()
    {
        if (Input.GetKeyUp(_screenGameplayMod.ActionKey))
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerTouchedTile = false;
        }
    }
}