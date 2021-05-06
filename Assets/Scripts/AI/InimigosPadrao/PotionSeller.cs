using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSeller : MonoBehaviour
{
	[SerializeField]
	GameSessionData GSD;
	[SerializeField]
	InventoryScript IS;
	[SerializeField]
	TrdControl TrdC;
	
	[SerializeField]
    IAWalk iawalk;
	
	[SerializeField]
	float playerDistance;

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(iawalk.target.transform.position, transform.position);
		
		if(playerDistance < 3 && Input.GetKeyDown("e") && GSD.coins > 0 && !GSD.hasPotion)
		{
			GSD.coins--;
			GSD.hasPotion = true;
			IS.EnablePotion();
			TrdC.EnablePotion();
		}
    }
}
