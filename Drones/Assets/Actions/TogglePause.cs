using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Action/Toggle Pause Action")]
public class TogglePause : BlankAction
{
    public GameObject menu;

    public override void OnTrigger()
    {
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
            if (menu != null) {
                menu.SetActive(true);
                LayoutRebuilder.MarkLayoutForRebuild(menu.GetComponent<RectTransform>());
            }
        }
        else
        {
            Time.timeScale = 1f;
            if (menu != null)
                menu.SetActive(false);
        }
    }
}
