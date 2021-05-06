using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMap : MonoBehaviour
{
	[SerializeField]
	GameSessionData GSD;
	[SerializeField]
	InventoryScript IS;
	
	[SerializeField]
    IAWalk iawalk;
	
	[SerializeField]
	float playerDistance;

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(iawalk.target.transform.position, transform.position);
		
		if(playerDistance < 3 && Input.GetKeyDown("e") && !GSD.hasMap)
		{
			GSD.hasMap = true;
			IS.EnableMap();
		}
    }
}
