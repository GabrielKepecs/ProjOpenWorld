using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	[SerializeField]
	Transform playerTransf;
	[SerializeField]
	TrdControl TrdC;
	[SerializeField]
	InventoryScript IS;
	
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
		
		/*if(other.gameObject.tag == "Ground")
		{
			TrdC.mayJump = true;
		}*/
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Pit")
		{
			playerTransf.position = new Vector3 (0, 0, 0);
		}
		
		if(other.gameObject.tag == "Helmet")
		{
			TrdC.EnableHelm();
			IS.EnableHelm();
		}
		
		if(other.gameObject.tag == "Key2")
		{
			IS.EnableKey2();
		}
	}
}
