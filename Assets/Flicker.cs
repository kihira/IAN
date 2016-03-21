using System;
using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour
{

    private float lastChange = 0;
    private float start;
    private Light light;

	// Use this for initialization
	void Start ()
	{
	    light = GetComponent<Light>();
	    start = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
	    if (lastChange > 2f)
	    {
	        light.intensity = Math.Abs(light.intensity) < 0.1f ? start : 0;
	        lastChange = 0;
	    }
	    lastChange += Time.deltaTime;
	}
}
