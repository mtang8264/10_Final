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
        InitStory();
    }

    void InitStory()
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
        ChoiceSelection();
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
        else 
        {
            story.ChooseChoiceIndex(choiceSelector);
            if (story.canContinue)
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
        choices.GetComponent<Animator>().SetBool("On", story.currentChoices.Count > 0 && done);

        if (story.currentChoices.Count == 0)
        {
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

    void ChoiceSelection()
    {
        if(story.currentChoices.Count == 0)
        {
            choiceSelector = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            choiceSelector++;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            choiceSelector--;

        if(choiceSelector < 0)
        {
            choiceSelector = story.currentChoices.Count - 1;
        }
        if(choiceSelector >= story.currentChoices.Count)
        {
            choiceSelector = 0;
        }
    }
}
