using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationSetupUI : MonoBehaviour {

    public FormationSetup formationSetup;

    public string formationName;

    public Transform droneTemplates;

    public Text formationSetupName;

    public Button addDrone;

    public Dropdown drone_selector;

    public GameObject droneSetupTemplate;

    public Transform target;

    private void Start()
    {
        formationSetupName.text = formationName;
        UpdateDropdown();
        addDrone.onClick.AddListener(MakeNewDroneTemplate);
        RebuildDroneTemplates();
    }

    public void RebuildDroneTemplates()
    {
        for (int i = 0; i < formationSetup.transform.childCount; i++)
            RebuildDroneTemplate(formationSetup.transform.GetChild(i));
    }

    public void RebuildDroneTemplate(Transform droneSetup)
    {
        MakeDroneTemplate(droneSetup);
    }

    public void MakeNewDroneTemplate()
    {
        string droneName = drone_selector.options[drone_selector.value].text;

        GameObject droneSetup = new GameObject();
        droneSetup.AddComponent<DroneSetup>();
        droneSetup.GetComponent<DroneSetup>().droneName = droneName;
        droneSetup.GetComponent<DroneSetup>().quantity = 1;
        droneSetup.transform.SetParent(formationSetup.transform);

        MakeDroneTemplate(droneSetup.transform);
    }

    public void MakeDroneTemplate(Transform droneSetup)
    {
        GameObject droneSetupUI = Instantiate(droneSetupTemplate);
        droneSetupUI.GetComponent<DroneSetupUI>().droneName = droneSetup.GetComponent<DroneSetup>().droneName;
        droneSetupUI.GetComponent<DroneSetupUI>().droneSetup = droneSetup.GetComponent<DroneSetup>();
        droneSetupUI.transform.SetParent(target);
    }

    public void UpdateDropdown()
    {
        List<string> drone_names = new List<string>();
        for (int i = 0; i < droneTemplates.childCount; i++)
            drone_names.Add(droneTemplates.GetChild(i).name);
        for (int i = 0; i < target.childCount; i++)
            drone_names.RemoveAll(s => {
                DroneSetupUI ui = target.GetChild(i).GetComponent<DroneSetupUI>();
                if (ui != null)
                    return ui.droneName.Equals(s);
                else
                    return false;
                });

        drone_selector.ClearOptions();
        drone_selector.AddOptions(drone_names);
        if(drone_selector.options.Count == 0)
        {
            drone_selector.interactable = false;
            addDrone.interactable = false;
        } else
        {
            drone_selector.interactable = true;
            addDrone.interactable = true;
        }
    }

    private void Update()
    {
        UpdateDropdown();
    }
}
