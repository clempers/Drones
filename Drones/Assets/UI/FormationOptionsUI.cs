using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationOptionsUI : ComponentUI {
    static public List<MetaFormationData> allowedFormations = new List<MetaFormationData>() { OrbitFormation.metaData, FreeFormation.metaData };

    public Dropdown formation_selector;

    public Button button;

    public Transform target;

    public GameObject newFormation;

    public Transform formations;

    public GameObject pendingCreator = null;

    private void Start()
    {
        formation_selector.ClearOptions();
        formation_selector.AddOptions(allowedFormations.ConvertAll<string>(meta => meta.name));
        button.onClick.AddListener(MakeNewFormation);
        Formation[] formationList = formations.GetComponentsInChildren<Formation>(true);
        foreach(Formation f in formationList)
        RebuildFormation(f);
    }

    private void RebuildFormation(Formation formation)
    {
        MetaFormationData metaData = allowedFormations.Find(x => formation.GetType() == x.formationType);
        if (metaData == null)
            return;
        GameObject newFormation = MakeFormation(metaData);
        newFormation.GetComponent<FormationUI>().realFormation = formation;
        Instantiate(metaData.creator(uiElements)).transform.SetParent(newFormation.transform);
    }

    private void MakeNewFormation()
    {
        string formation_name = formation_selector.options[formation_selector.value].text;

        MetaFormationData metaData = allowedFormations.Find(x => x.name.Equals(formation_name));
        GameObject newFormation = new GameObject();
        newFormation.transform.parent = formations;
        newFormation.AddComponent(metaData.formationType);
        GameObject formationUI = MakeFormation(metaData);
        formationUI.GetComponent<FormationUI>().realFormation = newFormation.GetComponent<Formation>();
        Instantiate(metaData.creator(uiElements)).transform.SetParent(formationUI.transform);
    }

    private GameObject MakeFormation(MetaFormationData metaData)
    {
        GameObject formation = Instantiate(newFormation);
        formation.transform.SetParent(target);
        formation.GetComponent<FormationUI>().SetState();
        formation.transform.localScale = new Vector3(1f, 1f, 1f);
        formation.GetComponent<FormationUI>().metaFormationData = metaData;
        return formation;
    }
}
