using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Choice : MonoBehaviour
{
    public string text;

    public bool show;

    public bool over;
    public bool click;
    private Animator animator;
    private TextMeshPro textMesh;

    public static string selectedTag = "<mspace=0.5em><color=#ffffff>;";
    public static string unselectedTag = "<mspace=0.5em><color=#aaaaaa>:";

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("On", show);

        textMesh.text = over ? selectedTag + text : unselectedTag + text;
    }

    private void OnMouseOver()
    {
        over = true;
    }
    private void OnMouseExit()
    {
        over = false;
    }
    private void OnMouseDown()
    {
        click = true;
    }
    private void OnMouseUp()
    {
        click = false;
    }
}
