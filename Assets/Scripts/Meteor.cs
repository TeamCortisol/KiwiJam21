using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float LeftForce = 10f;
    [SerializeField] float DownForce = 10f;

    public GameObject vfxExplode;
    public GameObject vfxSparkle;
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        var randomSpeedMod = Random.Range(0.5f, 1.5f);
        _rigidbody.velocity = new Vector2(randomSpeedMod * -LeftForce, randomSpeedMod * -DownForce);

        Destroy(gameObject, 20f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Player"))
        {
            var explosion = Instantiate(vfxExplode, transform.position, Quaternion.identity);
            Destroy(explosion, 3f);

            if (vfxSparkle != null)
            {
                var sparkle = Instantiate(vfxSparkle, transform.position, Quaternion.identity);
                Destroy(sparkle, 3f);
            }
            


            Destroy(gameObject);
        }
    }
}
