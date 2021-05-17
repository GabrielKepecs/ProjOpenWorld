using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
	public int health = 45;
	public BossMove BM;
	
	public bool pattern1, pattern2, pattern3;
	
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
		else if(!pattern1 && health <= 10)
		{
			pattern1 = true;
			
			BM.maySummon = true;
			BM.barrageCharges++;
		}
		else if(!pattern2 && health <= 30)
		{
			pattern2 = true;
			
			BM.cleaveCharges++;
			BM.barrageCharges++;
		}
		else if(!pattern3 && health <= 20)
		{
			pattern3 = true;
			
			BM.cleaveCharges++;
			BM.barrageCharges++;
		}
	}
}
