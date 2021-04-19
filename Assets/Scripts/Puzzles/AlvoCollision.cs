using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlvoCollision : MonoBehaviour
{
	[SerializeField]
	GameObject ponte;
	
    private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "PlayerProjectile")
		{
			ponte.SetActive(true);
		}
	}
}
