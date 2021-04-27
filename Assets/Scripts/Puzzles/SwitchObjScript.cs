using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObjScript : MonoBehaviour
{
	[SerializeField]
	Vector3 VectRotate;
	[SerializeField]
	float vMove, rotateSpd;
	
	public int currentPattern;
	
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(VectRotate * Time.deltaTime * rotateSpd);
		transform.position += Vector3.up * Mathf.Sin(Time.time) * vMove;
    }
}
