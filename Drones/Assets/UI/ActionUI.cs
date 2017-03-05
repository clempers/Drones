using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUI : ComponentUI {
    public MetaActionData metaActionData;

    public Component realAction;

    public Text title;

    private void Start()
    {
        SetState();
        title = GetComponentInChildren<Text>();
        title.text = metaActionData.name;
    }
}
