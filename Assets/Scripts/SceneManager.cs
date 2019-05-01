using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public TextAsset inkFile;
    public TextMeshPro text;
    public Choice[] choices;
    private Story story;
    public TextAsset[] scenes;

    private int storyIdx = 0;
    private string currentGoal;
    private string currentText;

    public float timePerCharacter;
    public bool done;
    private float timer;
    
    private string startTextCode = "<mspace=0.5em>";

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
        currentText = "";
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCommands();

        Type();
        WriteChoices();

        if (story.currentChoices.Count> 0)
            ChoiceSelection();

        if (Input.GetKeyDown(KeyCode.Return))
            Continue();
    }

    private void OnMouseDown()
    {
        if (story.currentChoices.Count > 0)
            return;
        Continue();
    }

    void Type()
    {
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

        text.text = startTextCode + currentText;
    }

    void CheckForCommands()
    {
        if(story.currentText.Substring(0,2) == "?V")
        {
            string temp = story.currentText.Substring(2).Trim();
            List<string> t = new List<string>(temp.Split('+'));
            Debug.Log("Attempting to show Visual called " + temp);
            for (int i = 0; i < Visuals.visuals.Count; i++)
            {
                Visuals.visuals[i].visible = t.Contains(Visuals.visuals[i].name);
                Debug.Log(t.Contains(Visuals.visuals[i].name) ? "Showed Visual " + Visuals.visuals[i].name : "Hid Visual " + Visuals.visuals[i].name);
            }
            story.Continue();
        }
    }

    void Continue()
    {
        if(done == false)
        {
            done = true;
            return;
        }
        if(story.canContinue)
        {
            story.Continue();
            currentText = "";
            text.text = "";
            done = false;
        }
        else
        {
            storyIdx++;
            if(storyIdx < scenes.Length)
            {
                inkFile = scenes[storyIdx];
                story = new Story(inkFile.text);
                story.Continue();
                currentText = "";
                text.text = "";
                done = false;
            }
        }
    }

    void WriteChoices()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            if (story.currentChoices.Count - 1 < i)
            {
                choices[i].show = false;
                continue;
            }
            choices[i].show = true;
            choices[i].text = story.currentChoices[i].text;
        }
    }

    void ChoiceSelection()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            if (choices[i].show == false)
                continue;
            if (choices[i].click == false)
                continue;

            story.ChooseChoiceIndex(i);
            Continue();
        }
    }
}