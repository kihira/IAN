using System;
using UnityEngine;
using System.Collections;
using Leap;

public class HandInfoPanel : MonoBehaviour
{
    [SerializeField] private HandController handController;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool faceCamera;
    [SerializeField] private float tolerance = 0.1f;
    [SerializeField] private float followSpeed = 0.1f;

    [SerializeField] private float retrackTime = 2f;
    private float detachedTime = 0f;

    private HandModel hand;
    private bool leftHand;

	void Update ()
	{
        // Attempt to reattach if possible
	    if (hand == null)
	    {
	        if (detachedTime > retrackTime)
	        {
	            Debug.Log("Failed to retrack correct hand, deactivating");
                Disable();
                return;
	        }
	        foreach (var graphicHand in handController.GetAllGraphicsHands())
	        {
	            if (graphicHand.GetLeapHand().IsLeft == leftHand)
	            {
                    hand = graphicHand;
                    detachedTime = 0f;
                    break;
	            }
	        }
	        detachedTime += Time.deltaTime;
	    }
        if (hand == null) return;

        // Less jittery to manually transform the position then add it as a child of the palm
        Vector3 newPos = hand.GetPalmPosition() + (hand.palm.up * 2f);
        if (Math.Abs(newPos.sqrMagnitude - transform.position.sqrMagnitude) > tolerance)
        {
            transform.Translate((newPos-transform.position)*followSpeed);
        }
        // TODO FIXME
        //if (faceCamera) transform.LookAt(Camera.main.transform);
	}

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Attach this panel to a certain hand
    /// The hand side is stored for future reference to re-attach to incase of temporary loss of tracking
    /// </summary>
    /// <param name="handModel">The HandModel to attach to</param>
    public void Attach(HandModel handModel)
    {
        hand = handModel;
        leftHand = handModel.GetLeapHand().IsLeft;
    }
}
