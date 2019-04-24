using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleSprites : MonoBehaviour
{
	public Sprite[] sprites;
	public float timePerSprite;
    private int current;
    private SpriteRenderer sprite;
	private float timer;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        timer = timePerSprite;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = timePerSprite;
            current++;
            if (current >= sprites.Length)
                current = 0;
        }
        sprite.sprite = sprites[current];
    }
}
