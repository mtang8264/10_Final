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

    public bool contentFilter;

    // Start is called before the first frame update
    void Start()
    {
        InitStory();
    }

    void InitStory()
    {
        story = new Story(inkFile.text);
        for (int i = 0; i < scenes.Length; i++)
        {
            if(inkFile.text == scenes[i].text)
            {
                storyIdx = i;
                break;
            }
        }
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

        if(contentFilter)
            currentGoal = Filter(currentGoal);

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

    string Filter(string t)
    {
        string curr = t;
        for (int i = 0; i < ContentFilter.filters.Length; i++)
        {
            if (curr.Contains(ContentFilter.filters[i].target) == false)
                continue;
            while(curr.Contains(ContentFilter.filters[i].target))
            {
                curr = curr.Replace(ContentFilter.filters[i].target, ContentFilter.filters[i].goal);
            }
        }
        return curr;
    }

    void CheckForCommands()
    {
        if(story.currentText.Substring(0,2) == "?V")
        {
            Debug.Log("Command to show Visual \"" + story.currentText.Substring(2).Trim() + "\".");
            string temp = story.currentText.Substring(2).Trim();
            List<string> t = new List<string>(temp.Split('+'));
            for (int i = 0; i < Visuals.visuals.Count; i++)
            {
                Visuals.visuals[i].visible = t.Contains(Visuals.visuals[i].name);
            }
            story.Continue();
        }

        if(story.currentText.Trim() == "?30")
        {
            Debug.Log("Command to load Scene 3_0.");
            for (int i = 0; i < scenes.Length; i++)
            {
                if(scenes[i].name == "Scene03_0")
                {
                    inkFile = scenes[i];
                }
            }
            storyIdx = 5;
            story = new Story(inkFile.text);
            story.Continue();
            currentText = "";
            text.text = "";
            done = false;
        }
        if (story.currentText.Trim() == "?31")
        {
            Debug.Log("Command to load Scene 3_1.");
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i].name == "Scene03_1")
                {
                    inkFile = scenes[i];
                }
            }
            storyIdx = 5;
            story = new Story(inkFile.text);
            story.Continue();
            currentText = "";
            text.text = "";
            done = false;
        }
        if (story.currentText.Trim() == "?32")
        {
            Debug.Log("Command to load Scene 3_2.");
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i].name == "Scene03_2")
                {
                    inkFile = scenes[i];
                }
            }
            storyIdx = 5;
            story = new Story(inkFile.text);
            story.Continue();
            currentText = "";
            text.text = "";
            done = false;
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