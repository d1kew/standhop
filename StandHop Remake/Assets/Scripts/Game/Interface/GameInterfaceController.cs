using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterfaceController : MonoBehaviour
{
    [SerializeField] private GameObject BuyMenu;
    [SerializeField] private GameObject[] Menus;

    public Image icon;

    public void BuyIsActive(bool flag)
    {
        BuyMenu.SetActive(flag);
    }

    public void InitWithMenu(int i)
    {
        foreach(GameObject obj in Menus)
        {
            obj.SetActive(false);
        }
        Menus[i].SetActive(true);
    }
}
