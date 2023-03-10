using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class PlayerInteract : MonoBehaviour
{
    public bool interactPossible = false;
    DialogueManager interactable;

    public TMPro.TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        if (interactPossible == true)
        {
            if (Input.GetButtonDown("Interact") && NPC==true )
            {
             
                interactPossible = false;
                Transform dialogue = interactable.transform.Find("Canvas/Dialogue");
                dialogue.gameObject.SetActive(true);
                interactable.startDialogue();

            }
            else if (Input.GetButtonDown("Interact") && NPC == false)
            {
                interactPossible = false;
                Transform dialogue = interactable.transform.Find("Canvas1/Dialogue");
                dialogue.gameObject.SetActive(true);

                interactable.isObject = true;

                interactable.startDialogue();
            }
        }
        else
        {
            if (interactable != null && Input.GetButtonDown("Interact"))
            {
                interactable.SkipText();
            }
        }
    }

    bool NPC = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("NPC"))
        {
            collision.GetComponentInChildren<TextMeshProUGUI>().SetText("Press E to interact");

            interactable = collision.GetComponent<DialogueManager>();

            NPC = true;

            interactPossible = true;
        }
        else if (collision.CompareTag("Object"))
        {
            collision.GetComponentInChildren<TextMeshProUGUI>().SetText("Press E to interact");

            interactable = collision.GetComponent<DialogueManager>();

            NPC = false;

            interactPossible = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            collision.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        }
        else if (collision.CompareTag("Object"))
        {
            collision.GetComponentInChildren<TextMeshProUGUI>().SetText("");
        }
    }
}
