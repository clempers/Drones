using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDropdownColor : MonoBehaviour {

    public CommonUIElements uiElements;

    public ColorCreator color_creator;

	// Use this for initialization
	void Start () {
        uiElements = color_creator.uiElements;
        Toggle toggle = GetComponent<Toggle>();
        Color c = uiElements.color_choices[transform.GetSiblingIndex() - 1];
        ColorBlock block = toggle.colors;
        block.highlightedColor = c;
        block.normalColor = c;
        block.pressedColor = c;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Toggle toggle = GetComponent<Toggle>();
        Color c = uiElements.color_choices[transform.GetSiblingIndex() - 1];
        ColorBlock block = toggle.colors;
        toggle.colors = updateColors(block, c);
    }

    public static ColorBlock updateColors(ColorBlock block, Color c)
    {
        block.highlightedColor = c;
        block.normalColor = c;
        block.pressedColor = c;
        return block;
    }
}
