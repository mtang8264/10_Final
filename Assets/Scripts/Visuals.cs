using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visuals : MonoBehaviour
{
    public static List<Visuals> visuals;
    public bool visible;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if(visuals == null)
        {
            visuals = new List<Visuals>();
        }
        visuals.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOpacity();
    }

    void UpdateOpacity()
    {
        if(visible)
        {
            sprite.color = Color.Lerp(sprite.color, new Color(1, 1, 1, 1), 0.1f);
        }
        else
        {
            sprite.color = Color.Lerp(sprite.color, new Color(1, 1, 1, 0), 0.1f);
        }
    }
}
