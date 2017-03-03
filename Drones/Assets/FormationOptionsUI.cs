using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationOptionsUI : MonoBehaviour {
    static public List<MetaFormationData> allowedFormations = new List<MetaFormationData>() { OrbitFormation.metaData, FreeFormation.metaData };

    public Dropdown formation_selector;

    public Button button;

    public Transform target;

    public GameObject newFormation;

    public Formation testFormation;

    private void Start()
    {
        formation_selector.ClearOptions();
        formation_selector.AddOptions(allowedFormations.ConvertAll<string>(meta => meta.name));
        button.onClick.AddListener(MakeNewFormation);
        RebuildFormation(testFormation);
    }

    private void RebuildFormation(Formation formation)
    {
        MetaFormationData metaData = allowedFormations.Find(x => formation.GetType() == x.formationType);
        GameObject newFormation = MakeFormation(metaData);
        newFormation.GetComponent<FormationUI>().realFormation = formation;
    }

    private void MakeNewFormation()
    {
        string formation_name = formation_selector.options[formation_selector.value].text;

        MetaFormationData metaData = allowedFormations.Find(x => x.name.Equals(formation_name));
        MakeFormation(metaData);
    }

    private GameObject MakeFormation(MetaFormationData metaData)
    {
        GameObject formation = Instantiate(newFormation);
        formation.transform.SetParent(target);
        formation.transform.localScale = new Vector3(1f, 1f, 1f);
        formation.GetComponent<FormationUI>().metaFormationData = metaData;
        return formation;
    }
}
