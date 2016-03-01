using UnityEngine;
using System.Collections;
using LMWidgets;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// PUT BUTTON IN CHILD OF EMPTY GAME OBJECT AND SET PARENT GAME OBJECT OF POSITION WHERE YOU WANT IT TO BE
/// Works around "issue" with default Leap button behaviour that resets x/y values
/// </summary>
[RequireComponent(typeof (Button), typeof (Image))]
public class ButtonToggle : ButtonToggleBase
{
    [SerializeField] private boolEvent onChangeEvent;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    // TODO support multiple transition types in the future?
    public override void ButtonTurnsOn()
    {
        UpdateSprite();
        Debug.Log("Button on");
        onChangeEvent.Invoke(true);
    }

    public override void ButtonTurnsOff()
    {
        UpdateSprite();
        Debug.Log("Button off");
        onChangeEvent.Invoke(false);
    }

    /// <summary>
    /// Sets the toggle state for the button
    /// </summary>
    public void SetToggleState(bool state, bool force = false, bool callEvents = false)
    {
        if (state == m_toggleState && !force) return;
        if (callEvents) ToggleState = state;
        else m_toggleState = state;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        GetComponent<Image>().overrideSprite = ToggleState ? GetComponent<Button>().spriteState.pressedSprite : null;
    }

    [System.Serializable]
    public class boolEvent : UnityEvent<bool> { }
}
