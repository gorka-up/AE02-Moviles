using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetController : MonoBehaviour, IPointerDownHandler
{
    public event Action Tapped;

    public void OnPointerDown(PointerEventData eventData)
    {
        Tapped.Invoke();
    }
}
