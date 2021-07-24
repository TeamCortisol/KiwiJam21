using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float JumpForce = 100f;

    private Rigidbody2D _rigidbody;
    private int _numberOfTimesGotHit = 0;    
    private ScreenGameplaySettings _screenGameplayMod;

    // Start is called before the first frame update
    void Start()
    {
        _screenGameplayMod = GetComponentInParent<ScreenGameplaySettings>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(_screenGameplayMod.ActionKey))
        {
            _rigidbody.AddForce(JumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _numberOfTimesGotHit++;
        Debug.Log($"You got hit {_numberOfTimesGotHit} times");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _numberOfTimesGotHit++;
        Debug.Log($"You got hit {_numberOfTimesGotHit} times");
    }
}
