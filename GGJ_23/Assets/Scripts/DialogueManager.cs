using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DialogStates
{
    Talking,
    Buttons,
    Waiting,
}

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] NPCMessage; //hello little chlild

    public string[] choices;

    public string[] replies;

    public float speed;

    public GameObject buttonPrefab;
    public GameObject buttonParent;
    public TextMeshProUGUI buttonText;

    public string[] objectDescription;

    GameObject[] buttons;

    public DialogStates state = DialogStates.Talking;

    public bool byeOption;

    public void getButtons()
    {
        buttons = new GameObject[choices.Length];
        for (int i = 0; i < choices.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, buttonParent.transform);
            buttons[i] = button;
            button.GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
            
            int j = i;
          
            button.GetComponent<Button>().onClick.AddListener(() => selectOption(j));
            

        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (state == DialogStates.Talking)
        {
            if (isDone)
            {
                if (byeOption == true)
                {
                    print("hel");
                    Canvas canvas = buttonParent.GetComponentInParent<Canvas>();
                    canvas.transform.Find("Dialogue").gameObject.SetActive(false);
                   
                    state = DialogStates.Waiting;
                }
               
                else
                {
                    state = DialogStates.Buttons;
                }
            }
        }
        else if (state == DialogStates.Buttons)
        {
            getButtons();
            
            state = DialogStates.Waiting;
        }
        else if (state == DialogStates.Waiting)
        {
           
        }
    }

    public void startDialogue()
    {
        StartCoroutine(writeText(NPCMessage));

    }
    
    public void presentAnswers()
    {
        StartCoroutine(writeText(replies));
    }

    public bool isDone = false;

    IEnumerator writeText(string[] message)
    {
        isDone = false;
        state = DialogStates.Talking;

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
        
    }

    public bool buttonsAgain = false;
    public void selectOption(int i)
    {
        //destroy buttons
        for (int k = 0; k < buttons.Length; k++)
        {
            Destroy(buttons[k]);
        }

        string[] replyList = replies[i].Split("[");
        if (i==choices.Length-1)
        {
            byeOption = true;
        }
        StartCoroutine(writeText(replyList));
    }
}

