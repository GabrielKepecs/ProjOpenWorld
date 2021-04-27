using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqBotao : MonoBehaviour
{
	[SerializeField]
	GameObject Cubo, Esfera, Piramide;
	
	[SerializeField]
	SwitchObjScript SOS;
	
	[SerializeField]
	int correctPattern;
	
	public int acertou;
	
	[SerializeField]
	float hitCD = 0, totalCD = 0.5f;

	private void Update()
	{
		if(hitCD > 0)
			hitCD -= Time.deltaTime;
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "PlayerProjectile" && hitCD <= 0)
		{
			switch(SOS.currentPattern)
			{
				case 0:
				Cubo.SetActive(false);
				Esfera.SetActive(true);
				Piramide.SetActive(false);
				SOS.currentPattern++;
				break;
				
				case 1:
				Cubo.SetActive(false);
				Esfera.SetActive(false);
				Piramide.SetActive(true);
				SOS.currentPattern++;
				break;
				
				default:
				Cubo.SetActive(true);
				Esfera.SetActive(false);
				Piramide.SetActive(false);
				SOS.currentPattern = 0;
				break;
			}
			
			if(SOS.currentPattern == correctPattern)
				acertou = 1;
			else
				acertou = 0;
			
			hitCD = totalCD;
		}
	}
}
