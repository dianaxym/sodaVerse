using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoemText : MonoBehaviour
{
    private TMP_Text poemText;
    private string curPoem;

    public string originPoem;

    // Start is called before the first frame update
    void Start()
    {
        poemText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        poemText.text = curPoem;
    }

    public void UpdatePoemText(int endIndex)
    {
        curPoem = originPoem.Substring(0, endIndex);
    }
    
}
