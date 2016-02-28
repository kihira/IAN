using System;
using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;

public class HandInfoPanel : MonoBehaviour
{
    [Header("Tracking Settings")]
    [SerializeField] private HandController handController;
    [SerializeField] private bool faceCamera;
    [SerializeField] private float retrackTime = 2f;
    private float detachedTime = 0f;

    [Header("UI")]
    [SerializeField] private GameObject canvas;

    private HandModel hand;
    private bool leftHand;

    private WallInteractable.LogData currLog;
    private float playingTime = 0f;

    private void Update()
    {
        // Attempt to reattach if possible
        if (hand == null)
        {
            if (detachedTime > retrackTime)
            {
                if (canvas.activeSelf)
                {
                    Debug.Log("Failed to retrack correct hand, deactivating canvas");
                    canvas.SetActive(false);
                }
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

        // Display downloading message if required


        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // Less jittery to manually transform the position then add it as a child of the palm
        Vector3 newPos = hand.GetPalmPosition() + (hand.palm.up * 0.1f);
        transform.position = Vector3.Lerp(transform.position, newPos, Math.Abs(newPos.sqrMagnitude - transform.position.sqrMagnitude));

        // TODO FIXME
        //if (faceCamera) transform.LookAt(Camera.main.transform);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Attach(HandModel handModel, WallInteractable.LogData log)
    {
        hand = handModel;
        leftHand = handModel.GetLeapHand().IsLeft;
        detachedTime = 0f;

        currLog = log;
        playingTime = 0f;

        // TODO show downloading message
        canvas.SetActive(true);
        GameObject.Find("Canvas/Text Panel/ScrollView/Text").GetComponent<Text>().text = log.message;
        GetComponent<AudioSource>().clip = log.audio;
        if (log.autoPlay) GetComponent<AudioSource>().Play();
    }
}
