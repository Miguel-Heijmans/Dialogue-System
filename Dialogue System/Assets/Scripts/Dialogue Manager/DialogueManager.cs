using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class DialogueManager : MonoBehaviour
{
    public Text textDisplay;
    public GameObject[] buttons;

    private JsonData dialogue;
    private int index;
    private string speaker;
    private JsonData currentLayer;

    private bool inDialogue;

    public bool loadDialogue(string path)
    {
        if(!inDialogue)
        {
            index = 0;
            var jsonTextFile = Resources.Load<TextAsset>("Dialogues/" + path);
            dialogue = JsonMapper.ToObject(jsonTextFile.text);
            currentLayer = dialogue;
            inDialogue = true;
            return true;
        }
        return false;
    }

    public bool printLine()
    {

        
        if(inDialogue)
        {
            JsonData line = currentLayer[index];

            foreach (JsonData key in line.Keys)
            {
                speaker = key.ToString();
            }

            if(speaker == "EOD")
            {
                inDialogue = false;
                textDisplay.text = "";
                return false;
            }
            else if(speaker == "q")
            {
                JsonData options = line[0];
                textDisplay.text = "";
                for (int optionsNumber = 0; optionsNumber < options.Count; optionsNumber++)
                {
                    activateButton(buttons[optionsNumber], options[optionsNumber]);
                }
            }
            else
            {
                dialogueTextColor(speaker);
                textDisplay.text = speaker +": " + line[0].ToString();
                index++;
            }
        }
        return true;
    }

    private void dialogueTextColor(string character)
    {
        textDisplay.color = GameObject.Find(character).GetComponent<Character>().getDialogueColor();
    }

    //button functions
    private void deactivateButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
            button.GetComponentInChildren<Text>().text = "";
            button.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    private void activateButton(GameObject button, JsonData choice)
    {
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = choice[0][0].ToString();
        button.GetComponent<Button>().onClick.AddListener(delegate { toDoOnClick(choice); });
    }

    void toDoOnClick(JsonData choice)
    {
        currentLayer = choice[0];
        index = 1;
        printLine();
        deactivateButtons();
    }


    private void Start()
    {
        deactivateButtons();
    }

}