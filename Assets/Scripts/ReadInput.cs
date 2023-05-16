using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadInput : MonoBehaviour
{
    public User user;

    private string nameInput;
    private TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadInputString()
    {
        nameInput = inputField.text;
        user.name = nameInput;
        ChatControl.instance.ChatTest(user);

        Debug.Log($"user input name: {nameInput}");
    }
}
