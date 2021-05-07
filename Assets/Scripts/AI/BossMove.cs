using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
	public NavMeshAgent agent;
    public GameObject target;
    public Animator anim;
	
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
    }
	public BossState currentState;
	public int phase;
	
	public float repositionSpeed;
	public float chargeStartSpeed, chargeSpeed;
	public float chargeTimer;
	public Vector3 repositionTarget, idlePosition;
	
	public float chargeAtkDistance;
	
	public float stunTimer, maxStunTimer;
	public bool stunned;
	
	public bool atkStarted;
	public float atkEndTimer;
	public bool atkEnd;
	public float atkEffectTimer;
	
	public GameObject BossProjectile;
	public float bulletSpawnTimer;
	
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
			/*case BossState.Reposition:
				Reposition();
				break;
				
			case BossState.Barrage:
				Barrage():
				break;
			case BossState.Cleave:
				Cleave():
				break;
			
			case BossState.Summon:
				Summon():
				break;*/
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
		if(!atkStarted)
		{
			anim.SetInteger("Attack", 1);
			anim.SetTrigger("AttackStart");
			
			atkEndTimer = 2;
			atkEffectTimer = bulletSpawnTimer;
			
			atkStarted = true;
		}
		
		if(atkEffectTimer > 0)
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
				
				currentState = BossState.Combat;
			}
			
			if(!atkEnd)
			{
				for(var i = 0; i < 5; i++)
				{
					Instantiate(BossProjectile,
								transform.position + new Vector3(2 - i, 1, 1),
								transform.rotation);
				}
				
				atkEnd = true;
			}
		}
	}
	
	void Charge()
	{
		if(!atkStarted)
		{
			
		}
	}
}
