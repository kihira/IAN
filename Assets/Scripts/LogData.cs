using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LogData
{
    public string title;
    [TextArea(5, 5)]
    public string message;
    public List<AudioClip> audio;
    public bool autoPlay;
}