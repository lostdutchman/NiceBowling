using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour {

    public GameObject Menu, OptionsMenu, SoundMenu;
    public UIFade MainMenuFade, OptionsMenuFade;
    private float UIFadeTime;
    public List<GameObject> ExpandableMenuItems;
    private List<UIExpandableObjects> MenuItems;

    void Start()
    {
        MenuItems = new List<UIExpandableObjects>();
        UIFadeTime = MainMenuFade.fadeTime;
        foreach(GameObject Item in ExpandableMenuItems)
        {
            UIExpandableObjects Temp = new UIExpandableObjects
            {
                Object = Item.gameObject,
                LocalScale = Item.gameObject.transform.localScale
            };
            MenuItems.Add(Temp);
        }
        OptionsMenu.SetActive(false);
        SoundMenu.SetActive(false);
    }

    public void ToggleOptionsMenu()
    {
        if (OptionsMenu.activeSelf)
        {
            StartCoroutine(FadeToMain());
        }
        else
        {
            StartCoroutine(FadeToOptions());
        }
    }

    public IEnumerator FadeToMain()
    {
        StartCoroutine(OptionsMenuFade.FadeOut());
        yield return new WaitForSecondsRealtime(UIFadeTime);
        OptionsMenu.SetActive(false);
        Menu.SetActive(true);
        ResetMenuSize();
        StartCoroutine(MainMenuFade.FadeIn());
    }

    public IEnumerator FadeToOptions()
    {
        StartCoroutine(MainMenuFade.FadeOut());
        yield return new WaitForSecondsRealtime(UIFadeTime);
        Menu.SetActive(false);
        OptionsMenu.SetActive(true);
        ResetMenuSize();
        StartCoroutine(OptionsMenuFade.FadeIn());
    }

    public void ResetMenuSize()
    {
        foreach (UIExpandableObjects Item in MenuItems)
        {
            try
            {
                Item.Object.transform.localScale = Item.LocalScale;
            }
            catch
            {
                //Sceen disabled do nothing for now
            }
        }
    }
}
