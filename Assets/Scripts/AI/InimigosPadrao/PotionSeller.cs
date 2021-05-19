using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionSeller : MonoBehaviour
{
	[SerializeField]
	GameSessionData GSD;
	[SerializeField]
	InventoryScript IS;
	[SerializeField]
	TrdControl TrdC;
	
	[SerializeField]
	GameObject TextBox, TBox, TextObj;
	[SerializeField]
	Text txt;
	
	[SerializeField]
    IAWalk iawalk;
	
	[SerializeField]
	bool playerNearby, potionReceived, thisNPC;
	[SerializeField]
	float playerDistance;

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(iawalk.target.transform.position, transform.position);
		
		if(playerDistance < 3) playerNearby = true;
		else playerNearby = false;
		
		if(playerNearby && !TextBox.activeSelf && !potionReceived)
		{
			TextBox.SetActive(true);
			TBox.SetActive(true);
			TextObj.SetActive(true);
			txt.text = "Press E to trade one coin for a potion.";
			thisNPC = true;
		}
		else if(!playerNearby && TextBox.activeSelf && thisNPC)
		{
			TextBox.SetActive(false);
			TBox.SetActive(false);
			TextObj.SetActive(false);
			thisNPC = false;
		}
		
		if(playerNearby && Input.GetKeyDown("e") && GSD.coins > 0 && !GSD.hasPotion)
		{
			GSD.coins--;
			GSD.hasPotion = true;
			IS.EnablePotion();
			TrdC.EnablePotion();
			
			potionReceived = true;
		}
    }
}
