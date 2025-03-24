using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using System.IO;
using System;

public class DialogueVariables : ScriptableObject
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    private Dictionary<string, Action> functionMap;
    //public DialogueVariables(string globalsFilePath)
    public DialogueVariables(TextAsset globalsFile)
    {
        Story globalVariablesStory = new Ink.Runtime.Story(globalsFile.text);

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value); 
            Debug.Log("Initialized global dialogue variable : " + name + " = " + value);
        }
    }

    private void Awake()
    {
        // Initialize dictionary with all existing functions
        functionMap = new Dictionary<string, Action>
        {
            { "DecreaseAllPlayersAllStatsBy50", PlayerStatManager.GetInstance().DecreaseAllPlayersAllStatsBy50 },
            { "IncreaseAllPlayersAllStatsBy100", PlayerStatManager.GetInstance().IncreaseAllPlayersAllStatsBy100 }
        };
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }


    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        // only maintain variables that were initialized from the globals ink file
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value );
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    // Verify and call function
    public bool CallFunction(string functionName)
    {
        //  Verify if the function name is mapped to a real function
        if (functionMap.TryGetValue(functionName, out Action function))
        {
            function(); // call function
            Debug.Log("Function found for : " + functionName);
            return true;
            
        }
        Debug.Log("Function not found for : " + functionName);
        return false;
    }

    public void TriggerDialogueEffect()
    {
        Ink.Runtime.Object variableValue = null;
        variables.TryGetValue("functionToCall", out variableValue);
        if ( variableValue != null )
        {
            Debug.Log("Searching for function :" + variableValue);
            CallFunction(variableValue.ToString());
        }
        else
        {
            Debug.Log("functionToCall variable not found in global ink variables");
        }
    }
}
