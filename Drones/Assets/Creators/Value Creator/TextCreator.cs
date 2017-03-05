using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCreator : ValueCreator<string>
{
    public InputField textField;

    public Text title;

    public string titleText;

    public override string GetValue()
    {
        return textField.text;
    }

    public override void SetValue(string v)
    {
        textField.text = v;
    }

    private void Start()
    {
        title.text = titleText;
        textField.onEndEdit.AddListener(r => saveCurrentValue(r));
    }
}
