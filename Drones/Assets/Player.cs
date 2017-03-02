using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public float rspeed = 3f;
    public Vector3 movement;
    public ControlCamera controlCamera;
    public FreeFormation freeFormation;
    public Formation attackFormation;

    public WorldState state;

    public void Awake()
    {
        state = new WorldState();
    }

    // Update is called once per frame
    void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movement = new Vector3(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        transform.Rotate(0, Input.GetAxis("Mouse X")*rspeed, 0);
        GetComponent<CharacterController>().SimpleMove(transform.rotation * movement);

        controlCamera.rotateVertical(Input.GetAxis("Mouse Y"));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            state.blue_laser_set = false;
            attackFormation.transform.SetParent(transform, true);
            attackFormation.relocate(freeFormation, transform.position);
        }
    }
}
