using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paradebugar : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
		transform.localPosition = new Vector3(0, 0.74f, 0);
        transform.localRotation = Quaternion.Euler(-90, 0, 90);
    }
}
