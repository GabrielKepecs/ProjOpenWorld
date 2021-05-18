using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paradebugar : MonoBehaviour
{
	public bool podebugar;
	
    // Update is called once per frame
    void LateUpdate()
    {
		transform.localPosition = new Vector3(0, 0.74f, 0);
		if(!podebugar)
			transform.localRotation = Quaternion.Euler(-90, 0, 90);
    }
}
