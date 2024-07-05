using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponBuyItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AnimationObject animation_object;
    [SerializeField] private Text cost_text, name_text;
    [SerializeField] private Image icon_holder;

    public void Awake()
    {
        icon_holder.sprite = animation_object.icon;
        cost_text.text = animation_object.cost.ToString();
        name_text.text = animation_object.name;
    }

    public void OnPointerClick(PointerEventData ped)
    {
        Set();
    }

    public void Set()
    {
        Object.FindObjectOfType<ArmsAnimationController>().Set(animation_object.name);
    }
}
