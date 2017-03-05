using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationSetupOptionsUI : MonoBehaviour {
    public Transform formation_templates;

    public Transform drone_templates;

    public Transform formation_setup_templates;

    public Transform formations;

    public Transform enemyFormations;

    public Transform enemyFormationTemplates;

    public Transform freeFormation;

    public Button addFormation;

    public Button buildFormations;

    public Button quitGame;

    public Dropdown selectFormation;

    public GameObject formationSetupTemplate;

    public Transform default_follow;

    public Transform target;

    public WorldState state;

    public void Start()
    {
        UpdateDropdown();
        addFormation.onClick.AddListener(CreateNewFormationSetup);
        buildFormations.onClick.AddListener(BuildFormations);
        RebuildFormationSetups();
        quitGame.onClick.AddListener(Application.Quit);
    }

    public void UpdateDropdown()
    {
        List<string> formation_names = new List<string>();
        for (int i = 0; i < formation_templates.childCount; i++)
            formation_names.Add(formation_templates.GetChild(i).name);
        selectFormation.ClearOptions();
        selectFormation.AddOptions(formation_names);
    }

    public void BuildFormations()
    {
        for (int i = 0; i < formations.childCount; i++)
            Destroy(formations.GetChild(i).gameObject);
        for (int i = 0; i < freeFormation.childCount; i++)
            Destroy(freeFormation.GetChild(i).gameObject);
        for (int i = 0; i < enemyFormations.childCount; i++)
        {
            Destroy(enemyFormations.GetChild(i).gameObject);
        }
        for (int i = 0; i < enemyFormationTemplates.childCount; i++)
        {
            GameObject newEnemyFormation = Instantiate(enemyFormationTemplates.GetChild(i).gameObject);
            newEnemyFormation.transform.SetParent(enemyFormations);
            newEnemyFormation.transform.localPosition = Vector3.zero;
        }

        state.ResetLasers();

        for(int i =0; i < formation_setup_templates.childCount; i++)
        {
            FormationSetup formationSetup = formation_setup_templates.GetChild(i).GetComponent<FormationSetup>();
            GameObject formationTemplate = null;
            for(int j = 0; j < formation_templates.childCount; j++)
            {
                if (formation_templates.GetChild(j).name.Equals(formationSetup.formation_name))
                    formationTemplate = formation_templates.GetChild(j).gameObject;
            }
            GameObject newFormation = Instantiate(formationTemplate);
            newFormation.GetComponent<Formation>().follow = default_follow;
            newFormation.transform.position = default_follow.position;
            newFormation.transform.SetParent(formations);
            for(int j = 0; j < formationSetup.transform.childCount; j++)
            {
                DroneSetup droneSetup = formationSetup.transform.GetChild(j).GetComponent<DroneSetup>();
                GameObject droneTemplate = null;
                for(int k =0; k < drone_templates.childCount; k++)
                {
                    if (drone_templates.GetChild(k).name.Equals(droneSetup.droneName))
                        droneTemplate = drone_templates.GetChild(k).gameObject;
                }
                for(int k = 0; k < droneSetup.GetComponent<DroneSetup>().quantity; k++)
                {
                    GameObject newDrone = Instantiate(droneTemplate);
                    newDrone.SetActive(true);
                    newFormation.GetComponent<Formation>().add_drone(newDrone.GetComponent<Drone>());
                    newDrone.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    public void Update()
    {
        UpdateDropdown();
    }

    public void RebuildFormationSetups()
    {
        for (int i = 0; i < formation_setup_templates.childCount; i++)
            RebuildFormationSetup(formation_setup_templates.GetChild(i));
    }

    public void RebuildFormationSetup(Transform child)
    {
        CreateFormationSetup(child);
    }

    public void CreateNewFormationSetup()
    {
        string formationName = selectFormation.options[selectFormation.value].text;

        GameObject formationSetup = new GameObject();
        formationSetup.name = formationName;
        formationSetup.AddComponent<FormationSetup>();
        formationSetup.GetComponent<FormationSetup>().formation_name = formationName;
        formationSetup.transform.SetParent(formation_setup_templates);

        CreateFormationSetup(formationSetup.transform);
    }

    public void CreateFormationSetup(Transform formationSetup)
    {
        GameObject formationSetupUI = Instantiate(formationSetupTemplate);
        formationSetupUI.transform.SetParent(target);
        formationSetupUI.GetComponent<FormationSetupUI>().formationName = formationSetup.GetComponent<FormationSetup>().formation_name;
        formationSetupUI.GetComponent<FormationSetupUI>().droneTemplates = drone_templates;
        formationSetupUI.GetComponent<FormationSetupUI>().formationSetup = formationSetup.GetComponent<FormationSetup>();
    }
}
