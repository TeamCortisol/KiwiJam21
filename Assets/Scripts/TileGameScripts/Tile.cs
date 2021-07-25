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
        // hack: start isn't called when this object is instantiated so call it manually here
        if (_screenGameplayMod == null)
        {
            Start();
            // another hack: keep doing this until it is not null
            return;  // don't even bother to do the rest of Update()
        }
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