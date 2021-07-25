using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSpawner : MonoBehaviour
{
    //public TextMesh textNumber;
    public int minNumber = 1;
    public int maxNumber = 5;
    //public Color ringColor = Color.red;

    public static NumberSpawner Instance;
    //public SpriteRenderer[] ringSprites;

    public int CurrentNumber { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    
        //StartCoroutine(SetRandomNumber(.5f));
    }

    public IEnumerator SetRandomNumber(float delay)
    {
        //textNumber.text = "";
        //foreach (var ring in ringSprites)
        //    ring.color = Color.white;

        yield return new WaitForSeconds(delay);
        GenerateNewNumber();
    }

    public int GenerateNewNumber()
    {
        CurrentNumber = Random.Range(minNumber, maxNumber);
        Debug.Log("currentNumber" + CurrentNumber);
        //textNumber.text = CurrentNumber.ToString();

        // visual
        //foreach (var ring in ringSprites)
        //    ring.color = Color.white;
        //ringSprites[CurrentNumber - 1].color = ringColor;

        return CurrentNumber;
    }
}
