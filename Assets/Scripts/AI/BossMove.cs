using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
	public NavMeshAgent agent;
    public GameObject target;
    public Animator anim;
	
	public TrdControl TC;
	
	public float aggroDistance;
	
	public int summonCharges, barrageCharges;
	
	public enum BossState
    {
        Stopped,
        Combat,
		Stunned,
		Dying,
		
		Shoot,
		Charge,
		Reposition,
		
		Summon,
		Barrage,
		Cleave,
		
		Test,
    }
	public BossState currentState;
	public int phase;
	
	public float baseSpeed, repositionSpeed, chargeSpeed;
	public float chargeTimer, cleaveTimer;
	public int reposPoint, cleaveAtkType;
	public Vector3 pointA, pointB, pointC;
	public Vector3 repositionTarget, idlePosition;
	
	public float chargeAtkDistance;
	public Transform swordTransform;
	public float lerpTimer;
	public float baseSwordY, chargeAtkSwordY, cleaveSwordY, baseSwordThick, cleaveSwordThick;
	
	public float stunTimer, maxStunTimer;
	public bool stunned;
	
	public bool atkStarted;
	public float atkEndTimer;
	public bool atkEnd;
	public float atkEffectTimer;
	
	public GameObject BossProjectile, BossHomingPrjct;
	public float bulletSpawnTimer, barrageTimer;
	public int barrageUses;
	
	public float deathTimer;
	public bool dying;
	
    // Start is called before the first frame update
    void Start()
    {
        stunTimer = maxStunTimer;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case BossState.Stopped:
                Stopped();
                break;
            case BossState.Combat:
                Combat();
                break;
            case BossState.Stunned:
                Stunned();
                break;
            case BossState.Dying:
                Dying();
                break;
				
            case BossState.Shoot:
                Shoot();
                break;
			case BossState.Charge:
				Charge();
				break;
			case BossState.Reposition:
				Reposition();
				break;
				
			case BossState.Barrage:
				Barrage();
				break;
			case BossState.Cleave:
				Cleave();
				break;
			
			/*case BossState.Summon:
				Summon();
				break;*/
				
			case BossState.Test:
				Test();
				break;
        }

        anim.SetFloat("Velocity", agent.velocity.magnitude);
    }
	
	void Stopped()
    {
        anim.SetInteger("Attack", 0);
		
		agent.SetDestination(idlePosition);
		
		if (Vector3.Distance(transform.position, target.transform.position) < aggroDistance)
        {
			currentState = BossState.Combat;
		}
	}
	
	void Combat()
	{
		anim.SetInteger("Attack", 0);
		
		agent.SetDestination(transform.position);
		
		if (Vector3.Distance(transform.position, target.transform.position) > aggroDistance)
        {
			currentState = BossState.Stopped;
		}
	}
	
	void Stunned()
	{
		agent.isStopped = true;
		
		if(!stunned)
		{
			anim.SetTrigger("Stun");
			stunned = true;
		}
		
		stunTimer -= Time.deltaTime;
		if(stunTimer <= 0)
		{
			stunTimer = maxStunTimer;
			
			anim.SetTrigger("ReturnToIdle");
			stunned = false;
			
			agent.isStopped = false;
			currentState = BossState.Combat;
		}
	}
	
	void Dying()
	{
		agent.isStopped = true;
		
		if(!dying)
		{
			anim.SetTrigger("Dead");
			dying = true;
		}
		
		deathTimer -= Time.deltaTime;
			if(deathTimer <= 0)
				Destroy(gameObject);
	}
	
	//ataques
	
	void Shoot()
	{
		if(!atkStarted)//variaveis usadas
		{
			anim.SetInteger("Attack", 1);
			anim.SetTrigger("AttackStart");
			
			atkEndTimer = 3;
			atkEffectTimer = bulletSpawnTimer;
			
			atkStarted = true;
			atkEnd = false;
		}
		
		if(atkEffectTimer > 0)//durante a animacao de ataque
		{
			atkEffectTimer -= Time.deltaTime;
			
			Rotate();
		}
		else//quando o boss vai atirar
		{
			atkEndTimer -= Time.deltaTime;
			if(atkEndTimer <= 0)//fim do ataque
			{
				atkStarted = false;
				anim.SetTrigger("ReturnToIdle");
				
				currentState = BossState.Combat;
			}
			
			if(!atkEnd)//tiro
			{
				for(var i = 0; i < 5; i++)
				{
					GameObject Bullet = Instantiate(BossProjectile,
										transform.position,
										transform.rotation);
					Vector3 spawnPoint = transform.TransformDirection(2 - i, 1, 1);
					Bullet.transform.position = Bullet.transform.position + spawnPoint;
				}
				
				atkEnd = true;
			}
		}
	}
	
	void Charge()
	{
		if(!atkStarted)
		{
			agent.SetDestination(target.transform.position);//vai ate o player
			agent.speed = baseSpeed;
			
			atkEffectTimer = 1;
			atkEndTimer = chargeTimer;
			lerpTimer = 0;
			
			atkStarted = true;
			atkEnd = false;
		}
		
		if(atkEffectTimer > 0)//start up do charge
		{
			atkEffectTimer -= Time.deltaTime;
		}
		else
		{
			atkEndTimer -= Time.deltaTime;
			if(atkEndTimer <= 0)
			{
				atkStarted = false;
				anim.SetTrigger("ReturnToIdle");
				
				currentState = BossState.Reposition;
			}
			
			if(!atkEnd)
			{
				agent.speed = chargeSpeed;//investida
				//se o jogador estiver perto, ataca
				if (Vector3.Distance(transform.position, target.transform.position) < chargeAtkDistance)
				{
					agent.SetDestination(transform.position);
					
					anim.SetInteger("Attack", 2);
					anim.SetTrigger("AttackStart");
					
					atkEndTimer = 2;
					atkEnd = true;
				}
			}
			else
			{
				SwordSizeChange(1, baseSwordY, chargeAtkSwordY);//aumenta o tamanho da espada
			}
		}
	}
	
	void Reposition()
	{
		if(!atkStarted)
		{
			agent.speed = repositionSpeed;
			
			atkEffectTimer = 1;
			lerpTimer = 0;
			
			atkStarted = true;
			
			reposPoint = Random.Range(0, 3);
			switch(reposPoint)
			{
				case 0:
				repositionTarget = pointA;
				break;
				
				case 1:
				repositionTarget = pointB;
				break;
				
				case 2:
				repositionTarget = pointC;
				break;
			}
			agent.SetDestination(repositionTarget);
		}
		//atkEndTimer <=0 agent.speed = baseSpeed;
		
		if(atkEffectTimer > 0)
		{
			atkEffectTimer -= Time.deltaTime;
			
			SwordSizeChange(1, chargeAtkSwordY, baseSwordY);//diminui o tamanho da espada
		}
		
		if(Vector3.Distance(transform.position, repositionTarget) < 1)
		{
			atkStarted = false;
			
			currentState = BossState.Combat;
		}
	}
	
	void Barrage()
	{
		if(!atkStarted)
		{
			anim.SetInteger("Attack", 3);
			
			atkEffectTimer = barrageTimer;
			atkEndTimer = 2;
			barrageUses = 4;
			
			atkStarted = true;
			atkEnd = false;
		}
		
		if(atkEffectTimer >= 0)
		{
			atkEffectTimer -= Time.deltaTime;
			
			Rotate();
			
			if(!atkEnd && barrageUses > 0)
			{
				anim.SetTrigger("AttackStart");
				atkEnd = true;
			}
		}
		else if(atkEffectTimer < 0 && barrageUses > 0)
		{
			atkEnd = false;
			
			GameObject Bullet = Instantiate(BossHomingPrjct,
											transform.position,
											transform.rotation);
			Vector3 spawnPoint = transform.TransformDirection(2, 2, 1);
			Bullet.transform.position = Bullet.transform.position + spawnPoint;
			
			atkEffectTimer = barrageTimer;
			barrageUses--;
		}
		else
		{
			atkEndTimer -= Time.deltaTime;
			if(atkEndTimer <= 0)
			{
				atkStarted = false;
				anim.SetTrigger("ReturnToIdle");
				
				currentState = BossState.Combat;
			}
		}
	}
	
	void Cleave()
	{
		if(!atkStarted)
		{
			anim.SetTrigger("CleaveAlert");
			
			atkEffectTimer = 3;
			atkEndTimer = cleaveTimer;
			barrageUses = 6;
			lerpTimer = 0;
			cleaveAtkType = 0;
			
			atkStarted = true;
			atkEnd = false;
		}
		
		if(atkEffectTimer > 0)
		{
			atkEffectTimer -= Time.deltaTime;
			
			CleaveSizeChange(2, baseSwordY, cleaveSwordY, baseSwordThick, cleaveSwordThick);
		}
		else
		{
			Rotate();
			
			if(Vector3.Distance(transform.position, target.transform.position) < 4)
			{
				//stun no player
				//TC.GetPushed(10);//empurra o player de volta pro range do boss
			}
			
			atkEndTimer -= Time.deltaTime;
			if(atkEndTimer <= 0 && barrageUses <= 0)
			{
				atkStarted = false;
				
				currentState = BossState.Stunned;
			}
			
			else if(atkEndTimer <= 0 && barrageUses > 0)
			{
				atkEndTimer = cleaveTimer;
				barrageUses--;
				
				switch(cleaveAtkType)//faz o boss atacar em um padrao
				{
					case 0:
					cleaveAtkType++;
					anim.SetInteger("Attack", 2);
					anim.SetTrigger("AttackStart");
					break;
					
					case 1:
					cleaveAtkType++;
					anim.SetInteger("Attack", 5);
					anim.SetTrigger("AttackStart");
					break;
					
					case 2:
					cleaveAtkType = 0;
					anim.SetInteger("Attack", 4);
					anim.SetTrigger("AttackStart");
					break;
				}
			}
			
			if(barrageUses <= 0)
			{
				CleaveSizeChange(3, cleaveSwordY, baseSwordY, cleaveSwordThick, baseSwordThick);
			}
		}
	}
	
	void Rotate()
	{
		Vector3 direction = (target.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 6);
	}
	
	void SwordSizeChange(float lerpDuration, float initialSize, float endSize)
	{
		if(lerpTimer < lerpDuration)
			lerpTimer += Time.deltaTime;
		
		swordTransform.localScale = new Vector3
						(1, Mathf.Lerp(initialSize, endSize, lerpTimer / lerpDuration), 1);
	}
	
	void CleaveSizeChange(float lerpDuration, float initialSize, float endSize, float initialThickness, float endThickness)
	{
		if(lerpTimer < lerpDuration)
			lerpTimer += Time.deltaTime;
		
		swordTransform.localScale = new Vector3
						(Mathf.Lerp(initialThickness, endThickness, lerpTimer / lerpDuration),
						 Mathf.Lerp(initialSize, endSize, lerpTimer / lerpDuration),
						 Mathf.Lerp(initialThickness, endThickness, lerpTimer / lerpDuration));
	}
	
	void Test()
	{
		
	}
}