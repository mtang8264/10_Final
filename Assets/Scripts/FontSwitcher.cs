using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontSwitcher : MonoBehaviour
{
    public TMP_FontAsset[] fonts;
    public float avgPerFont;
    public float variance;

    private float timer;
    private int selection = 0;
    private TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        timer = avgPerFont + Random.Range(-variance, variance);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = avgPerFont + Random.Range(-variance, variance);
            selection++;
            if(selection >= fonts.Length)
            {
                selection = 0;
            }
        }

        text.font = fonts[selection];
    }
}
