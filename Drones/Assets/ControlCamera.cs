using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour {
    public float focal_distance;

   public void rotateVertical(float degrees)
    {
        transform.localRotation *= Quaternion.AngleAxis(degrees, Vector3.left);
        transform.localPosition = transform.localRotation * new Vector3(0f, 0f, -focal_distance) ;
    }
}
