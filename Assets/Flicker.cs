using System;
using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour
{
    private float lastChange = 0;
    private bool shouldRun;

    private Color colour = Color.red;
    private float startValue;
    private Light light;

    public bool ShouldRun
    {
        get { return shouldRun; }
        set
        {
            shouldRun = value;
            if (shouldRun)
            {
                Color startColor = light.color;
                light.color = colour;
                colour = startColor;
            }
        }
    }

    // Use this for initialization
	void Start ()
	{
	    light = GetComponent<Light>();
	    startValue = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
	    if (shouldRun && lastChange >= 1f)
	    {
	        light.intensity = Math.Abs(light.intensity) < 0.1f ? startValue : 0;
	        lastChange = 0;
	    }
	    lastChange += Time.deltaTime;
	}
}
