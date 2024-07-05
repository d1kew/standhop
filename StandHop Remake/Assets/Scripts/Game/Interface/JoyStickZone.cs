using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickZone : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameObject joy;

    public void OnPointerDown(PointerEventData eventData)
    {
        joy.SetActive(true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        joy.SetActive(false);
    }
}
