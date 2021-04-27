using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
	[SerializeField]
	bool activate;
	
	[SerializeField]
	float moveTimer = 3;
	[SerializeField]
	float moveSpeed = 2, fountainSpeed;
	
	public GameSessionData GSD;
	
	void Update()
    {
		if(activate && moveTimer > 0)
		{
			fountainSpeed = moveSpeed * Time.deltaTime;
			transform.Translate(0, fountainSpeed, 0);
			moveTimer -= Time.deltaTime;
		}
	}
	
    private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "PlayerProjectile" && GSD.hasSecondKey)
		{
			activate = true;
		}
	}
}
