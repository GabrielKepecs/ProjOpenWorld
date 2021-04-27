using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDirTrigger : MonoBehaviour
{
	[SerializeField]
	Box box;
	[SerializeField]
	int directionSwitch;
	
    private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Box"))
			box.pathDirection = directionSwitch;
	}
}
