using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberCreator : ValueCreator<float>
{
    public InputField textField;

    public Text title;

    public string titleText;

    public override float GetValue()
    {
        return float.Parse(textField.text);
    }

    public override void SetValue(float v)
    {
        textField.text = v.ToString();
    }

    private void Start()
    {
        title.text = titleText;
        textField.onEndEdit.AddListener(r => saveCurrentValue(float.Parse(r)));
    }
}
