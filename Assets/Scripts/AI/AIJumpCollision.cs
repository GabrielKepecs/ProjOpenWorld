using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJumpCollision : MonoBehaviour
{
    public IAWalk iawalk;
	
	private void OnCollisionEnter(Collision other)
	{
		if(iawalk.jumping && other.gameObject.tag == "Ground")
		{
			//faz a aranha nao se teleportar de volta pro comeco do pulo
			iawalk.agent.nextPosition = transform.position;
			
			//reativa o agent
			iawalk.agent.updatePosition = true;
			iawalk.agent.updateRotation = true;
			
			//desativa o movimento do rigid
			iawalk.rigid.isKinematic = true;
			
			//encerra o pulo no IAWalk
			iawalk.stopJump = true;
		}
	}
}
