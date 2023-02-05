using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] NPCMessage; //hello little chlild

    public string[] choices;

    public string[] replies;

    public float speed;

    public TextMeshProUGUI button1;
    public TextMeshProUGUI button2;
    public TextMeshProUGUI button3;



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

    public bool isDone = false;

    IEnumerator writeText(string[] message)
    {

        
        for (int i = 0; i < message.Length; i++)
        {
            for (int k = 0; k < message[i].Length; k++)
            {
                if (message[i][k] == '<')
                {

                    while (message[i][k] != '>')
                    {
                        text.text += message[i][k];
                        i++;
                    }
                    text.text += message[i][k];

                }
                text.text += message[i][k];

                yield return new WaitForSeconds(speed);
            }

            yield return new WaitForSeconds(0.75f);

            text.text = "";
            
        }

        isDone = true;

    }

    public void presentOptions()
    {
        button1.text = choices[0];
        button2.text = choices[1];
        button3.text = choices[2];
    }
}

