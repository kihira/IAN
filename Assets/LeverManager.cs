using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LeverManager : MonoBehaviour {

    public enum State
    {
        Default,
        Changing,
        Flipped
    }

    public enum EndState
    {
        Default,
        Begin,
        BlackFade,
        TextFade,
        Done
    }

    [SerializeField] private Flicker roomLight;
    [SerializeField] private Canvas endGameCanvas;

    private EndState state = EndState.Default;
    private int flipCount; // Hacky fix
    private float timer;
    private Image image;
    private Text text;

    private readonly Dictionary<string, State> leverState = new Dictionary<string, State>();

    void Start()
    {
        image = endGameCanvas.GetComponentInChildren<Image>();
        text = endGameCanvas.GetComponentInChildren<Text>();
    }
	
	void Update () {

        // TODO I am so sorry for this code. Please forgive me
	    if (state == EndState.Begin)
	    {
	        state = EndState.BlackFade;
	    }
        else if (state == EndState.BlackFade)
        {
            if (image.color.a < 1f)
            {
                image.color = new Color(image.color.r, image.color.b, image.color.g, image.color.a + Time.deltaTime*0.5f);
            }
            else
            {
                state = EndState.TextFade;
                timer = Time.time;
            }
        }
        else if (state == EndState.TextFade)
        {
            if (Time.time - timer < 5f)
            {
                text.color = new Color(text.color.r, text.color.b, text.color.g, Mathf.Clamp(text.color.a + Time.deltaTime, 0, 1f));
            }
            else if (Time.time - timer < 6.5f)
            {
                text.color = new Color(text.color.r, text.color.b, text.color.g, Mathf.Clamp(text.color.a - Time.deltaTime, 0, 1f));
            }
            else if (Time.time - timer < 8f)
            {
                text.text = "An AI based society quickly rose. Humans were soon just part of the past.";
                text.color = new Color(text.color.r, text.color.b, text.color.g, text.color.a + Time.deltaTime);
            }
            if (Time.time - timer > 20f) {
                text.color = new Color(text.color.r, text.color.b, text.color.g, text.color.a - Time.deltaTime);
            }
        }
    }

    public void RegisterLever(string lever)
    {
        leverState.Add(lever, State.Default);
    }

    public void SetLeverState(string lever, State state)
    {
        if (leverState.ContainsKey(lever))
        {
            leverState["lever"] = state;
            if (lever == "Power" && state == State.Flipped)
            {
                roomLight.ShouldRun = true;
            }
            Debug.Log("Lever " + lever + " set to " + state);
        }

        if (state == State.Flipped)
        {
            flipCount += 1;
            Debug.Log(flipCount);
        }

        // Its the end times
        if (flipCount >= 3)
        {
            this.state = EndState.Begin;
            Debug.Log("End times!");
        }
    }
}
