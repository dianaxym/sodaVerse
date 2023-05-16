using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using System.Text;

public class ChatControl : MonoBehaviour
{
    public static ChatControl instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ChatTest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public static async void ChatTest()
    //{
    //    StringBuilder sb = new StringBuilder();

    //    var api = new OpenAIClient();
    //    var chatPrompts = new List<Message>
    //    {

    //        new Message("user", "write a poem of 25 words without punctuation"),

    //    };
    //    var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);

    //    await api.ChatEndpoint.StreamCompletionAsync(chatRequest, result =>
    //    { 
    //        sb.Append(result.Choices[0].ToString());
    //    });


    //    Debug.Log(sb.ToString());

    //}

    public async void ChatTest(User user)
    {
        StringBuilder sb = new StringBuilder();

        var api = new OpenAIClient();
        var chatPrompts = new List<Message>
        {
            new Message("user", $"write a poem about a person named {user.name} in 2 words without any punctuation or numbers."),

        };
        var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);

        await api.ChatEndpoint.StreamCompletionAsync(chatRequest, result =>
        {
            sb.Append(result.Choices[0].ToString());
        });

        user.UpdateUserPoem(sb.ToString());
        Debug.Log($"the poem for {user.name} is : {user.poem}");

    }
}
