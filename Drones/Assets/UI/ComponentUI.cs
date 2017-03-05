using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentUI : MonoBehaviour
{
    public WorldState state;

    public CommonUIElements uiElements;

    public void SetState()
    {
        state = transform.parent.GetComponentInParent<ComponentUI>().state;
        uiElements = transform.parent.GetComponentInParent<ComponentUI>().uiElements;
    }
}
