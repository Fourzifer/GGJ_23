using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string NPCMessage; //hello little chlild

    public string[] choices;

    public string[] replies;

    public float speed;

    public TextMeshProUGUI button1;

   
    // Start is called before the first frame update
    void Start()
    {
  
        
    }

    private void Update()
    {
        
    }

    public void startDialogue()
    {
        StartCoroutine(writeText(NPCMessage));

    }

    void continueDialogue()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            StartCoroutine(writeText(choices[i]));
        }
    }

    IEnumerator writeText(string message)
    {
      
        for (int i = 0; i < message.Length; i++)
        {
            if (message[i] == '<')
            {
               
                while (message[i] != '>')
                {
                    text.text += message[i];
                    i++;
                }
                text.text += message[i];

            }
            text.text += message[i];

            yield return new WaitForSeconds(speed);

        }


    
    }

    void presentOptions()
    {
        button1.text = choices[0];
    }




}

