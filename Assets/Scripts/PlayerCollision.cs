using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	[SerializeField]
	Transform playerTransf;
	
    private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Water")
		{
			if(playerTransf.position.x > 271)
			{
				playerTransf.position = new Vector3 (380, 6, 260);
			}
			else
			{
				playerTransf.position = new Vector3 (216, 8, 186);
			}
		}
	}
}
