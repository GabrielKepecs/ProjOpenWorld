using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
	public int health;
	public BossMove BM;
	
    private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("PlayerProjectile"))
		{
			health--;
		}
		if (health <= 0)
        {
            BM.currentState = BossMove.BossState.Dying;
        }
	}
}
