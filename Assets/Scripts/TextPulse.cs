using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPulse : MonoBehaviour
{
    private RectTransform t;
    public float min;
    public float max;
    public float phase;
    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        scale = Mathf.Sin((Time.time * phase) * Mathf.PI);
        scale += 1;
        scale /= 2;
        scale = Mathf.Lerp(min, max, scale);
        t.localScale = new Vector3(scale, scale, 1);
    }
}
