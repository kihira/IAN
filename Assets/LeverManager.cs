using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeverManager : MonoBehaviour {

    public enum State
    {
        Default,
        Changing,
        Flipped
    }

    private readonly Dictionary<string, State> leverState = new Dictionary<string, State>(); 
	
	void Update () {
	
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
            Debug.Log("Lever " + lever + " set to " + state);
        }

        // Check all states to see what state it triggers
        foreach (KeyValuePair<string, State> pair in leverState)
        {
            if (pair.Value != State.Flipped) return;
        }
        
    }
}
