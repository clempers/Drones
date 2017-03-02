using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    public bool powered = true;

    private void Update()
    {
        GetComponent<Collider>().enabled = powered;
        GetComponent<Renderer>().enabled = powered;
    }
}
