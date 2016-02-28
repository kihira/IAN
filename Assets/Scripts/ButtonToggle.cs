using UnityEngine;
using System.Collections;
using LMWidgets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// PUT BUTTON IN CHILD OF EMPTY GAME OBJECT AND SET PARENT GAME OBJECT OF POSITION WHERE YOU WANT IT TO BE
/// Works around "issue" with default Leap button behaviour that resets x/y values
/// </summary>
[RequireComponent(typeof (Button), typeof (Image))]
public class ButtonToggle : ButtonToggleBase
{

    // TODO support multiple transition types in the future?
    public override void ButtonTurnsOn()
    {
        Button button = GetComponent<Button>();
        if (button.transition == Selectable.Transition.SpriteSwap)
        {
            GetComponent<Image>().overrideSprite = GetComponent<Button>().spriteState.pressedSprite;
        }
        GetComponent<Button>().OnPointerClick(new PointerEventData(null));
    }

    public override void ButtonTurnsOff()
    {
        Button button = GetComponent<Button>();
        if (button.transition == Selectable.Transition.SpriteSwap)
        {
            GetComponent<Image>().overrideSprite = null;
        }
        GetComponent<Button>().OnPointerClick(new PointerEventData(null));
    }
}
