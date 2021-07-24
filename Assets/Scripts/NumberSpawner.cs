using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSpawner : MonoBehaviour
{
    public TextMesh textNumber;
    public int minNumber = 1;
    public int maxNumber = 5;

    public static NumberSpawner Instance;

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
    
        StartCoroutine(SetRandomNumber(.5f));
    }

    public IEnumerator SetRandomNumber(float delay)
    {
        textNumber.text = "";
        yield return new WaitForSeconds(delay);
        GenerateNewNumber();
    }

    public void GenerateNewNumber()
    {
        CurrentNumber = Random.Range(minNumber, maxNumber);
        Debug.Log("currentNumber" + CurrentNumber);
        textNumber.text = CurrentNumber.ToString();
    }
}
