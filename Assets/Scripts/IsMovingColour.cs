using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsMovingColour : MonoBehaviour
{
    TouchLaunch player;
    Color green;
    Color red;
    // Start is called before the first frame update
    void Start()
    {
        green = new Color(0,255,0,255);
        red = new Color(255, 0, 0, 255);
        player = GameObject.Find("Player").GetComponent<TouchLaunch>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsStatic && player.isGrounded)
        {
            gameObject.GetComponent<Image>().color = green;
        }
        else
        {
            gameObject.GetComponent<Image>().color = red;
        }
    }
}
