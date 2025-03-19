using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choices;
    public bool isDialoguePlaying { get; set; }
    private Story currentStory;
    private TextMeshProUGUI[] choicesText; // track the text for each choice
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Dialog instance already exists");
        }
        instance = this;
    }

    private void Start()
    {
        isDialoguePlaying = false;
        dialogPanel.SetActive(false);

        // get all text choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDialoguePlaying) { return; }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void EnterDialogMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogPanel.SetActive(true);
        isDialoguePlaying = true;

        ContinueStory();

    }

    private void ExitDialogMode()
    {
        // hide pannel
        dialogPanel.SetActive(false);
        isDialoguePlaying = false;
        dialogueText.text = ""; // clear text
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue) // verify if the story ended or not
        {
            dialogueText.text = currentStory.Continue(); // display next line in the story

            // display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            ExitDialogMode(); // end of story, ends DialogMode
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given " + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        // 
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }
}
