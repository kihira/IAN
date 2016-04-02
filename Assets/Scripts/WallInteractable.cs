using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class WallInteractable : MonoBehaviour
{
    [SerializeField] private LogData log;

    private bool activated = false;

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

    void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        HandModel hand = GetHand(other.transform);
        if (hand == null || hand.GetLeapHand().IsLeft) return;

        // Attach hand info panel and activate
        GameObject handPanel = GameObject.Find("Hand Mount");
        handPanel.GetComponent<HandInfoPanel>().Attach(hand, log);
        activated = true;

        Debug.Log("Hand activated wall panel");
    }
}
