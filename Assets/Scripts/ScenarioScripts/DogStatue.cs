using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogStatue : MonoBehaviour
{
	[SerializeField]
	GameObject PortalShr4;
	
	public GameSessionData GSD;
	
	[SerializeField]
	bool shrineSpawned;
	
    private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "PlayerProjectile" && GSD.hasTargetKey && !shrineSpawned)
		{
			PortalShr4.SetActive(true);
			shrineSpawned = false;
		}
	}
}
