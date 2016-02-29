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

    private WallInteractable.LogData currLog;
    private float playingTime = 0f;

    void Update()
    {
        // Attempt to reattach if possible
        if (hand == null)
        {
            if (detachedTime > retrackTime && canvas.activeSelf)
            {
                Debug.Log("Failed to retrack correct hand, deactivating canvas");
                canvas.SetActive(false);
                return;
            }
            foreach (var graphicHand in handController.GetAllGraphicsHands())
            {
                if (!graphicHand.GetLeapHand().IsLeft)
                {
                    hand = graphicHand;
                    detachedTime = 0f;
                    break;
                }
            }
            detachedTime += Time.deltaTime;
        }
        if (hand == null) return;

        UpdatePosition();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canvas.activeSelf && other.GetComponent<HandModel>() != null && other.GetComponent<HandModel>().GetLeapHand().IsLeft)
        {
            canvas.SetActive(true);
        }
    }

    private void UpdatePosition()
    {
        // Less jittery to manually transform the position then add it as a child of the palm
        // Palm position isn't in middle of palm, more it floats up and in front of it. not going insane trying to correct for that
        Vector3 newPos = hand.GetPalmPosition() + transform.parent.rotation * (Vector3.left * 0.1f);
        transform.position = Vector3.Lerp(transform.position, newPos, Math.Abs(newPos.sqrMagnitude - transform.position.sqrMagnitude));

        // TODO FIXME
        //if (faceCamera) transform.LookAt(Camera.main.transform);
    }

    public void Attach(HandModel handModel, WallInteractable.LogData log)
    {
        hand = handModel;
        detachedTime = 0f;

        currLog = log;
        playingTime = 0f;

        // TODO show downloading message
        canvas.SetActive(true);
        GameObject.Find("Canvas/Text Panel/ScrollView/Text").GetComponent<Text>().text = log.message;
        GameObject.Find("Canvas/Text Panel/ScrollHandle").GetComponent<DynamicScroll>().DelayedUpdate(0.1f);
        GetComponent<AudioSource>().clip = log.audio;
        if (log.autoPlay) GetComponent<AudioSource>().Play();
    }
}
