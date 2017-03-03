using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewFormation : MonoBehaviour {
    public Button button;
    public Transform target;
    public GameObject newTrigger;

    private void Start()
    {
        button.onClick.AddListener(CreateTrigger);
    }

    void CreateTrigger()
    {
        Instantiate(newTrigger).transform.SetParent(target);
    }
}
