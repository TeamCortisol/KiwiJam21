using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInstruction : MonoBehaviour
{
    public GameObject instructionText;
    
    // Start is called before the first frame update
    void Start()
    {
        ShowInstructions();
        StartCoroutine(RemoveInstructions());
    }

    void ShowInstructions()
    {
        instructionText.SetActive(true);
    }
    
    IEnumerator RemoveInstructions()
    {
        yield return new WaitForSeconds(3f);
        instructionText.SetActive(false);
    }
}
