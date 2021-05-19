using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveMap : MonoBehaviour
{
	[SerializeField]
	GameSessionData GSD;
	[SerializeField]
	InventoryScript IS;
	
	[SerializeField]
	GameObject TextBox, TBox, TextObj;
	[SerializeField]
	Text txt;
	
	[SerializeField]
    IAWalk iawalk;
	
	[SerializeField]
	bool playerNearby, mapReceived, thisNPC;
	[SerializeField]
	float playerDistance;

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(iawalk.target.transform.position, transform.position);
		
		if(playerDistance < 3) playerNearby = true;
		else playerNearby = false;
		
		if(playerNearby && !TextBox.activeSelf && !mapReceived)
		{
			TextBox.SetActive(true);
			TBox.SetActive(true);
			TextObj.SetActive(true);
			txt.text = "Press E to receive a map.";
			thisNPC = true;
		}
		else if(!playerNearby && TextBox.activeSelf && thisNPC)
		{
			thisNPC = false;
			TextBox.SetActive(false);
			TBox.SetActive(false);
			TextObj.SetActive(false);
		}
		
		if(playerNearby && Input.GetKeyDown("e") && !GSD.hasMap)
		{
			GSD.hasMap = true;
			IS.EnableMap();
			
			mapReceived = true;
		}
    }
}
