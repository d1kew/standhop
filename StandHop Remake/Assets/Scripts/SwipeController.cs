using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private GameObject Change;
    private ArmsAnimationController anim;

    private void Start()
    {
        anim = Object.FindObjectOfType<ArmsAnimationController>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if(eventData.delta.x < 0)
            {
                if(anim.currentWeapon != 2)
                {
                    anim.SetInWeaponary(anim.knife_object);
                    anim.currentWeapon = 2;
                    return;
                }
                else anim.Inspect();
            }
            if(eventData.delta.x > 0)
            {
                if(anim.currentWeapon != 1)
                {
                    anim.SetInWeaponary(anim.pistol_object);
                    anim.currentWeapon = 1;
                    return;
                }
                else anim.Inspect();
            }
        }
        else
        {
            if(eventData.delta.y < 0)
            {
                if(anim.currentWeapon != 3)
                {
                    anim.SetInWeaponary(anim.weapon_object);
                    anim.currentWeapon = 3;
                    return;
                }
                else anim.Inspect();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Change.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Change.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        anim.Inspect();
    }    
}
