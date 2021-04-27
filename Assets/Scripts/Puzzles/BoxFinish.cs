using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFinish : MonoBehaviour
{
	[SerializeField]
	Box box;
	
	bool finish;
	
	[SerializeField]
	GameObject HelmetPickup;
	
    private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Box") && !finish)
		{
			box.boxSpeed = 0;
			finish = true;
			Instantiate(HelmetPickup, transform.position + new Vector3(-2, 1.2f, -8.5f), Quaternion.identity);
		}
	}
}
