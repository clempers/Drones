using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonUIElements : MonoBehaviour {
    public GameObject orbitFormationCreator;
    public GameObject shoulderFormationCreator;
    public GameObject shieldWallFormationCreator;
    public GameObject freeFormationCreator;

    public GameObject laserTargetCreator;

    public GameObject moveFormationTargetCreator;
    public GameObject laserTargetActionCreator;

    public GameObject basicDroneCreator;

    public GameObject pendingCreator = null;

    public List<Color> color_choices;

    public FreeFormation freeFormation;

    public List<RectTransform> menu_columns;

    private void Update()
    {
        float sum = 0;
        for(int i =0; i < menu_columns.Count; i++)
        {
            menu_columns[i].anchoredPosition = new Vector3(sum, 0);
            sum += menu_columns[i].sizeDelta.x;
        }
    }
}
