using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScrollingTextbox : MonoBehaviour
{
    public float speed = 10;                                    //Characters per second
    public float characterTime { get { return 1 / speed; } }    //Seconds per character

    public string text
    {
        set
        {
            //Reset everything
            textObj.text = "";

            m_text = value;
            textPos = 0;
            countdown = characterTime;
        }

        get
        {
            return m_text;
        }
    }
    private string m_text = "";

    private Text textObj;
    private int textPos = 0;
    private float countdown;

    //Events

    void Awake()
    {
        textObj = GetComponent<Text>();

        //Set the countdown
        countdown = characterTime;
    }

    void Update()
    {
        if (textPos < text.Length)
        {
            //Count down to add a new one
            countdown -= Time.deltaTime;

            while (countdown <= 0 && textPos < text.Length)
            {
                countdown += characterTime;
                textObj.text += text[textPos];

                textPos++;
            }
        }
    }
}
