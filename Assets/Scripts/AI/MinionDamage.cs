using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDamage : MonoBehaviour
{
	public int hp;
	public MinionMove MM;
	
    private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("PlayerProjectile"))
		{
			hp--;
		}
		if(hp <= 0 || other.gameObject.tag == "Water")
		{
			MM.currentState = MinionMove.MinionState.Dying;
		}
		
		if(MM.jumping && other.gameObject.tag == "Ground")
		{
			//faz o minion nao se teleportar de volta pro comeco do pulo
			MM.agent.nextPosition = transform.position;
			
			//reativa o agent
			MM.agent.updatePosition = true;
			MM.agent.updateRotation = true;
			
			//desativa o movimento do rigid
			MM.rigid.isKinematic = true;
			
			//encerra o pulo no IAWalk
			MM.stopJump = true;
		}
	}
}
