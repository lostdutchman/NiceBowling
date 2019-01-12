using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour {

    public GameObject Menu, OptionsMenu;



    public void ToggleOptionsMenu()
    {
        if (OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(false);
            Menu.SetActive(true);
        }
        else
        {
            OptionsMenu.SetActive(true);
            Menu.SetActive(false);
        }
    }
}
