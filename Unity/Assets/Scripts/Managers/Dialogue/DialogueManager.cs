using Ink.Parsed;
using Ink.Runtime;
using Ink.UnityIntegration;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choices;
    public bool isDialoguePlaying { get; set; }
    private Ink.Runtime.Story currentStory;
    private TextMeshProUGUI[] choicesText; // track the text for each choice

    // handling variables change
    private DialogueVariables dialogueVariables;
    [SerializeField] private InkFile globalsInkFile;


    public static DialogueManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Dialog instance already exists");
            return;
        }

        instance = this;

        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
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


    public void EnterDialogMode(TextAsset inkJSON)
    {
        // pause game timer to stop all other events
        TimeManager.GetInstance().PauseTimerByDialogue();

        currentStory = new Ink.Runtime.Story(inkJSON.text);
        dialogPanel.SetActive(true);
        isDialoguePlaying = true;

        // listen for variable change
        dialogueVariables.StartListening(currentStory);

        ContinueStory();
    }

    private void ExitDialogMode()
    {
        // start game timer to continue all other events
        TimeManager.GetInstance().StartTimerByDialogue();

        // stop listening for variable change
        dialogueVariables.StopListening(currentStory);

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
        List<Ink.Runtime.Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given " + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Ink.Runtime.Choice choice in currentChoices)
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

    public void MakeChoice(int  choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        dialogueVariables.TriggerDialogueEffect();

        StartCoroutine(WaitSeconds());
    }

    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2);
        ExitDialogMode();
    }

    // Get the value of a specific ink variable
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink variable was found to be null " +  variableName);
        }
        return variableValue;
    }


}
