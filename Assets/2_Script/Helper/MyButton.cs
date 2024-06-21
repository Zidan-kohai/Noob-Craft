using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : Button, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnHandUp;
    public event Action OnHandDown;


    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        OnHandDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        OnHandUp?.Invoke();
    }
}