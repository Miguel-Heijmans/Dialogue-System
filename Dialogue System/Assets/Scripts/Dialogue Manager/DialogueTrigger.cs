using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager DialogueManager;
    public GameObject player;
    public string dialoguePath;

    private bool inTrigger = false;
    private bool dialogueLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (DialogueManager == null)
        {
            DialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            inTrigger = false;
        }
    }

    private void runDialogue(bool keyTrigger)
    {
        if(keyTrigger)
        {
            if (inTrigger && !dialogueLoaded)
            {
                dialogueLoaded = DialogueManager.loadDialogue(dialoguePath);
            }

            if (dialogueLoaded)
            {
                dialogueLoaded = DialogueManager.printLine();
            }
        }
    }

    void Update()
    {
        runDialogue(Input.GetKeyDown(KeyCode.C));
        
    }
}
