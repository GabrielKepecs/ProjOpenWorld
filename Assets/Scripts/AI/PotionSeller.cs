using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSeller : MonoBehaviour
{
	[SerializeField]
	GameSessionData GSD;
	
	[SerializeField]
    IAWalk iawalk;
	
	[SerializeField]
	float playerDistance;
	
	[SerializeField]
	GameObject Potion;

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(iawalk.target.transform.position, transform.position);
		
		if(playerDistance < 3 && Input.GetKeyDown("e") && GSD.hasCoin)
		{
			GSD.hasCoin = false;
			GSD.hasPotion = true;
		}
    }
}
