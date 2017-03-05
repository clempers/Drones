using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneSetupUI : MonoBehaviour
{
    public DroneSetup droneSetup;

    public string droneName;

    public Text formationSetupName;

    public InputField droneNumber;

    private void Start()
    {
        formationSetupName.text = droneName;
        droneNumber.text = droneSetup.quantity.ToString();
        droneNumber.onEndEdit.AddListener(s => droneSetup.quantity = int.Parse(s));
    }
}
