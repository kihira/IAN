using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class HandInfoPanel : MonoBehaviour
{
    [Header("Tracking Settings")]
    [HideInInspector] public HandController handController;
    [SerializeField] private bool faceCamera;
    [SerializeField] private float retrackTime = 2f;
    private float detachedTime = 0f;
    private bool shouldReattach = false;

    [Header("UI")]
    [SerializeField] private GameObject canvas;

    [Header("Audio")]
    [SerializeField] private GameObject audioButton;

    [Header("Logs")]
    [SerializeField] private GameObject logButtonPrefab;
    [SerializeField] private GameObject logList;

    /** Components **/
    private AudioQueue audioQueue;
    private AudioSource audioSource;
    private int currAudioPos;

    private HandModel hand;
    private List<LogData> logs = new List<LogData>();
    private LogData currLog;
    private float playingTime = 0f;

    void Start()
    {
        audioQueue = GetComponent<AudioQueue>();
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    void Update()
    {
        // Attempt to reattach if possible
        if (hand == null && shouldReattach)
        {
            if (detachedTime > retrackTime)
            {
                Debug.Log("Failed to retrack correct hand, giving up");
                shouldReattach = false;
                return;
            }
            foreach (var graphicHand in handController.GetAllGraphicsHands())
            {
                if (!graphicHand.GetLeapHand().IsLeft)
                {
                    hand = graphicHand;
                    detachedTime = 0f;
                    canvas.SetActive(true);
                    break;
                }
            }
            detachedTime += Time.deltaTime;
        }
        if (hand == null)
        {
            if (canvas.activeSelf) canvas.SetActive(false);
            return;
        }

        UpdatePosition();
    }

    void FixedUpdate()
    {
        Audio();
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

    public void Attach(HandModel handModel, LogData log)
    {
        hand = handModel;
        detachedTime = 0f;
        shouldReattach = true;

        currLog = log;
        playingTime = 0f;

        canvas.SetActive(true);
        GameObject.Find("Canvas/Text Panel/ScrollView/Text").GetComponent<Text>().text = log.message;
        GameObject.Find("Canvas/Text Panel/ScrollHandle").GetComponent<DynamicScroll>().DelayedUpdate(0.1f);
        if (log.audio != null) audioQueue.AddAudio(log.audio, true);

        logs.Add(log);
        AddLogButton(log);
    }

    void AddLogButton(LogData data, bool sort = true)
    {
        // Create and setup button object
        GameObject logButton = Instantiate(logButtonPrefab);
        logButton.transform.SetParent(logList.transform, false);
        logButton.GetComponentInChildren<Text>().text = data.title;

        if (sort)
        {
            logs.Sort((l1, l2) => l1.message.Length - l2.message.Length);
            // Remove all children then readd them
            for (int i = 0; i < logList.transform.childCount; i++)
            {
                Destroy(logList.transform.GetChild(i).gameObject);
            }
            foreach (LogData log in logs)
            {
                AddLogButton(log, false);
            }
        }
    }

    void Audio()
    {
        if (audioButton.GetComponent<ButtonToggle>().ToggleState != audioSource.isPlaying)
        {
            audioButton.GetComponent<ButtonToggle>().ToggleState = audioSource.isPlaying;
        }
    }

    public void AudioToggle(bool start)
    {
        if (start)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void LogButton(string title)
    {
        
    }
}
