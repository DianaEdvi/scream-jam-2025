using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : Button
{
    public static Action OnMenuButtonFlickering;
    public static Action OnMenuButtonSteady;
    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        OnMenuButtonFlickering?.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        OnMenuButtonSteady?.Invoke();
    }
}
