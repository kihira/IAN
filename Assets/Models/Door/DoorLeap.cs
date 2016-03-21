using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class DoorLeap : MonoBehaviour {

	bool Open; 

	HandModel GetHand(Transform other)
	{
		HandModel hand = null;
		if (other == null)
		{
			return null;
		}
		hand = other.GetComponent<HandModel>();
		return hand ?? GetHand(other.transform.parent);
	}


	void OnTriggerEnter(Collider collider)
	{
		if (Open == true)
			return;

		HandModel hand = GetHand (collider.transform);
		if (collider.gameObject.tag == "Player" || hand == null || hand.GetLeapHand ().IsLeft) 
		{
			this.GetComponent<Animator> ().SetTrigger ("Open");
			Open = true;
		}	
			return;
	}

	void
}