using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUI : MonoBehaviour {
    public MetaActionData metaActionData;

    public Text title;

    private void Start()
    {
        title.text = metaActionData.name;
    }
}
