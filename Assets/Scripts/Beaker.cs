using UnityEngine;
using System.Collections;

public class Beaker : MonoBehaviour {

    private bool broken;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
        if (!broken)
        {
            GetComponent<Animator>().SetBool("broken", true);
            broken = true;
        }
    }
}
