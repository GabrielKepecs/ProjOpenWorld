using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour
{
	[SerializeField]
	GameSessionData GSD;
	
	[SerializeField]
	GameObject TextBox, TBox, TextObj;
	[SerializeField]
	Text txt;
	
	[SerializeField]
	bool playerNearby, thisNPC;
	[SerializeField]
	float playerDistance;

	[SerializeField]
	GameObject Target, Boss;

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(Target.transform.position, transform.position);
		
		if(playerDistance < 7) playerNearby = true;
		else playerNearby = false;
		
		if(playerNearby && !TextBox.activeSelf)
		{
			TextBox.SetActive(true);
			TBox.SetActive(true);
			TextObj.SetActive(true);
			if(!GSD.hasSword)
				txt.text = "Return here once you have the five sword pieces.";
			else
				txt.text = "Press E to challenge yourself.";
			thisNPC = true;
		}
		else if(!playerNearby && TextBox.activeSelf && thisNPC)
		{
			thisNPC = false;
			TextBox.SetActive(false);
			TBox.SetActive(false);
			TextObj.SetActive(false);
			thisNPC = false;
		}
		
		if(playerNearby && Input.GetKeyDown("e") && GSD.hasSword)
		{
			Boss.SetActive(true);
			
			thisNPC = false;
			TextBox.SetActive(false);
			TBox.SetActive(false);
			TextObj.SetActive(false);
			
			Destroy(gameObject);
		}
    }
}
