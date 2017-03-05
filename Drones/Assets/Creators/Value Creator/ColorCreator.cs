using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCreator : ValueCreator<Color>
{
    public Dropdown color_choice;

    private void Start()
    {
        color_choice.ClearOptions();
        color_choice.AddOptions(uiElements.color_choices.ConvertAll(c => ""));
        color_choice.onValueChanged.AddListener(i =>
        {
            color_choice.colors = ChangeDropdownColor.updateColors(color_choice.colors, uiElements.color_choices[i]);
            saveCurrentValue(uiElements.color_choices[i]);
        });
        SetValue(uiElements.color_choices[color_choice.value]);
        saveCurrentValue(uiElements.color_choices[color_choice.value]);
        //color_choice.colors = ChangeDropdownColor.updateColors(color_choice.colors, formationUI.color_choices[0]);
    }
    public void SetOptions()
    {
        color_choice.ClearOptions();
        color_choice.AddOptions(uiElements.color_choices.ConvertAll(c => ""));
    }

    public override Color GetValue()
    {
        return uiElements.color_choices[color_choice.value];
    }

    public override void SetValue(Color v)
    {
        color_choice.colors = ChangeDropdownColor.updateColors(color_choice.colors, v);
        color_choice.value = uiElements.color_choices.FindIndex(c => c.Equals(v));
    }
}
