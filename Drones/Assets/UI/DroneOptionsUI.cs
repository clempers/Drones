using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneOptionsUI : ComponentUI {
    static public List<MetaDroneData> allowedDrones = new List<MetaDroneData>() { BasicDrone.metaData };

    public Dropdown drone_selector;

    public Button button;

    public Transform target;

    public GameObject newDrone;

    public Transform droneTemplates;

    private void Start()
    {
        drone_selector.ClearOptions();
        drone_selector.AddOptions(allowedDrones.ConvertAll<string>(meta => meta.name));
        button.onClick.AddListener(MakeNewDrone);
        Drone[] droneList = droneTemplates.GetComponentsInChildren<Drone>(true);
        Debug.Log(droneList.Length);
        foreach (Drone f in droneList)
            RebuildDrone(f);
    }

    private void RebuildDrone(Drone drone)
    {
        MetaDroneData metaData = allowedDrones.Find(x => drone.GetType() == x.droneType);
        if (metaData == null)
            return;
        GameObject newDrone = MakeDrone(metaData);
        newDrone.GetComponent<DroneUI>().realDrone = drone;
        Instantiate(metaData.creator(uiElements)).transform.parent = newDrone.transform;
    }

    private void MakeNewDrone()
    {
        string drone_name = drone_selector.options[drone_selector.value].text;

        MetaDroneData metaData = allowedDrones.Find(x => x.name.Equals(drone_name));

        GameObject droneUI = MakeDrone(metaData);

        GameObject droneCreator = Instantiate(metaData.creator(uiElements));
        droneCreator.transform.SetParent(droneUI.transform);
        GameObject newDrone = Instantiate(droneCreator.GetComponent<ComponentCreator>().objectTemplate);
        newDrone.transform.parent = droneTemplates;
        droneUI.GetComponent<DroneUI>().realDrone = newDrone.GetComponent<Drone>();
    }

    private GameObject MakeDrone(MetaDroneData metaData)
    {
        GameObject drone = Instantiate(newDrone);
        drone.transform.SetParent(target);
        drone.GetComponent<DroneUI>().SetState();
        drone.transform.localScale = new Vector3(1f, 1f, 1f);
        drone.GetComponent<DroneUI>().metaDroneData = metaData;
        return drone;
    }
}
