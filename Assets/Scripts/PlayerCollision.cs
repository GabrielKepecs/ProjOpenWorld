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
		/*if(other.gameObject.tag == "Water" && !TrdC.GSD.hasSword)
		{
			if(playerTransf.position.x > 271)
			{
				playerTransf.position = new Vector3 (380, 6, 260);
			}
			else
			{
				playerTransf.position = new Vector3 (216, 8, 186);
			}
		}*/
		
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
		
		if(other.gameObject.tag == "Carrot")
		{
			IS.EnableCarrot();
			TrdC.moveMod = 1.3f;
		}
		
		if(other.gameObject.tag == "Shield")
		{
			TrdC.EnableShield();
			IS.EnableShield();
		}
		
		if(other.gameObject.tag == "Safira")
		{
			TrdC.GSD.swordPieces++;
			if(TrdC.GSD.swordPieces >= 5)
				TrdC.GSD.hasSword = true;
			IS.EnableSafira();
			Destroy(other.gameObject);
		}
		if(other.gameObject.tag == "Pomo")
		{
			TrdC.GSD.swordPieces++;
			if(TrdC.GSD.swordPieces >= 5)
				TrdC.GSD.hasSword = true;
			IS.EnablePomo();
			Destroy(other.gameObject);
		}
		if(other.gameObject.tag == "Lamina")
		{
			TrdC.GSD.swordPieces++;
			if(TrdC.GSD.swordPieces >= 5)
				TrdC.GSD.hasSword = true;
			IS.EnableLamina();
			Destroy(other.gameObject);
		}
		if(other.gameObject.tag == "Guarni")
		{
			TrdC.GSD.swordPieces++;
			if(TrdC.GSD.swordPieces >= 5)
				TrdC.GSD.hasSword = true;
			IS.EnableGuarni();
			Destroy(other.gameObject);
		}
		if(other.gameObject.tag == "Cabo")
		{
			TrdC.GSD.swordPieces++;
			if(TrdC.GSD.swordPieces >= 5)
				TrdC.GSD.hasSword = true;
			IS.EnableCabo();
			Destroy(other.gameObject);
		}
	}
}
