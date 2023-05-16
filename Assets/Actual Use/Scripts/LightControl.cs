using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light targetlight;
    public Color color1;
    public Color color2;
    public float changeSpeed;
    float startSpeed;

    public User user1;
    public User user2;

    // Start is called before the first frame update
    void Start()
    {

     
    }

    // Update is called once per frame
    void Update()
    {
        if(user1.progress >= 0.9 || user2.progress >= 0.9)
        {
            changeSpeed = 0.006f;
        } else if(user1.progress >= 0.75 || user2.progress >= 0.75)
        {
            changeSpeed = 0.004f;
        } else if(user1.progress >= 0.5 || user2.progress >= 0.5)
        {
            changeSpeed *= 0.002f;
        }

        LightColorChange();
    }

    void LightColorChange()
    {
        targetlight.GetComponent<Light>().color = Color.Lerp(color1, color2, Mathf.PingPong(startSpeed, 1));
        if (startSpeed < 10)
        {
            startSpeed += changeSpeed;
        }
        else
        {
            startSpeed = 0;
        }
        
        
    }
}
