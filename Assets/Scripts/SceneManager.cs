using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public TextAsset inkFile;
    public TextMeshPro text;
    public TextMeshPro choices;
    private Story story;

    private string currentGoal;
    private string currentText;

    public float timePerCharacter;
    public bool done;
    private float timer;

    public string selectedText;
    public string unselectedText;
    private int choiceSelector;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkFile.text);
        story.Continue();
        timer = timePerCharacter;
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Type();
        WriteChoices();

        if (Input.GetKeyDown(KeyCode.Return))
            Continue();
    }

    void Type()
    {
        currentText = text.text;
        currentGoal = story.currentText;

        if(currentText.Length != currentGoal.Length && !done)
        {
            timer -= timePerCharacter;
            if(timer <= 0)
            {
                currentText = currentGoal.Substring(0, currentText.Length + 1);
                timer = timePerCharacter;
                done = currentText.Equals(currentGoal);
            }
        }
        else if(done)
        {
            currentText = currentGoal;
        }

        text.text = currentText;
    }

    void Continue()
    {
        if(done == false)
        {
            done = true;
            return;
        }
        if(story.currentChoices.Count == 0)
        {
            if(story.canContinue)
            {
                story.Continue();
                currentText = "";
                text.text = "";
                done = false;
            }
        }
    }

    void WriteChoices()
    {
        if(story.currentChoices.Count == 0)
        {
            choices.text = "";
            return;
        }
        string temp = "";
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            temp += choiceSelector == i ? selectedText : unselectedText;
            temp += story.currentChoices[i].text + "</color>" + '\n';
        }
        choices.text = temp;
    }
}
