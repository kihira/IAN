using UnityEngine;
using System.Collections;
using Leap;

public class WallInteractable : MonoBehaviour
{

    [SerializeField] private GameObject handInfoPanel;

    private int currHandTouching = -1;

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
        // TODO way to allow replaying?
        if (currHandTouching != -1) return;

        var hand = GetHand(other.transform);
        if (hand == null) return;

        var handID = hand.GetLeapHand().Id;
        currHandTouching = handID;

        // Attach hand info panel and activate
        handInfoPanel.GetComponent<HandInfoPanel>().Attach(hand);
        handInfoPanel.SetActive(true);

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
