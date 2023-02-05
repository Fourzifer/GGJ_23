using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerInteract : MonoBehaviour
{
    public bool interactPossible = false;
    DialogueManager interactable;

    public TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool isInteracting = false;
    // Update is called once per frame
    void Update()
    {

        if (interactPossible == true)
        {
           
            if (Input.GetButtonDown("Interact"))
            {

                
                interactPossible = false;
                Transform dialogue = interactable.transform.Find("Canvas/Dialogue");
                dialogue.gameObject.SetActive(true);
                interactable.startDialogue();
                isInteracting = true;

            }

        }


        if (isInteracting)
        {
            if (interactable.isDone)
            {
                interactable.presentOptions();
                interactable.transform.Find("Canvas/Options").gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("NPC")|| collision.CompareTag("NPC"))
        {
            text.SetText("Press E to interact");

            interactable = collision.GetComponent<DialogueManager>();
            interactPossible = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            text.SetText("");
        }

    }
}
