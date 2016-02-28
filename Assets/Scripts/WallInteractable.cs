using System;
using UnityEngine;
using System.Collections;
using Leap;

public class WallInteractable : MonoBehaviour
{
    [Serializable]
    public struct LogData
    {
        public string title;
        [TextArea(5, 5)] public string message;
        public AudioClip audio;
        public bool autoPlay;
    }

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

/*    void OnTriggerStay(Collider other)
    {
        if (currHandTouching == -1) return;
        else Debug.Log("Hand colliding");
    }

    void OnTriggerExit(Collider other)
    {
        HandModel hand_model = GetHand(other.transform);
        if (hand_model != null && hand_model.GetLeapHand().Id == currHandTouching)
        {
            Debug.Log("Hand leave " + currHandTouching);
            currHandTouching = -1;
        }
    }*/
}
