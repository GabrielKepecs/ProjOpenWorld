using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMove : MonoBehaviour
{
	public UnityEngine.AI.NavMeshAgent agent;
	public GameObject target;
	public Animator anim;
	public Rigidbody rigid;
	
	public GameSessionData GSD;
	
	public Vector3 patrolposition;
	public float stoppedTime;
    public float patrolDistance;//=10;
    public float timetowait;// = 3;
    public float aggroDistance, disengageDistance;// = 10;
    public float attackDistance, atkStopDistance;// = 3;
	
	public enum MinionState
	{
		Stopped,
		Patrol,
		Dying,
		Aggro,
		
		Attack,
		Jump,
		Shoot,
		Flying,
	}
	public MinionState currentState;
	
	public GameObject Drop;
	public bool dead;
	public float deathTimer;
	
	public string minionType;//jumper, shooter (base e homing), flying (voa na direcao do player), shadow (aleatorio)
	public float baseSpeed, aggroSpeed;
	
	public bool jumping, stopJump;
	public Vector3 JumpForce;
	public float jumpTimer, maxJT;
	public float atkCD, maxAtkCD;
	public float atkSpawnX, atkSpawnY, atkSpawnZ;
	public GameObject Projectile, BasicShot, HomingShot, Boss;
	
	public bool randomize, bossMinion;
	public int randomizeType;
	
    // Start is called before the first frame update
    void Start()
    {
        patrolposition = new Vector3(transform.position.x + Random.Range(-patrolDistance, patrolDistance), transform.position.y, transform.position.z + Random.Range(-patrolDistance, patrolDistance));
		
		if(randomize)
		{
			switch (randomizeType = Random.Range(0, 4))
			{
				case 0:
				minionType = "Jumper";
				break;
				
				case 1:
				minionType = "ShooterB";
				break;
				
				case 2:
				minionType = "ShooterH";
				break;
				
				case 3:
				minionType = "Flier";
				break;
			}
		}
		
		if(bossMinion)
		{
			Boss = GameObject.FindWithTag("Boss");
			target = GameObject.FindWithTag("Player");
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(currentState)
		{
			case MinionState.Stopped:
			Stopped();
			break;
			
			case MinionState.Patrol:
			Patrol();
			break;
			
			case MinionState.Dying:
			Dying();
			break;
			
			case MinionState.Aggro:
			Aggro();
			break;
			
			case MinionState.Attack:
			Attack();
			break;
			
			case MinionState.Jump:
			Jump();
			break;
			
			case MinionState.Shoot:
			Shoot();
			break;
			
			case MinionState.Flying:
			Flying();
			break;
		}
		
		anim.SetFloat("Velocity", agent.velocity.magnitude);
    }
	
	void Stopped()
	{
		agent.isStopped = true;
        //anim.SetBool("Attack", false);

        if (target && Vector3.Distance(transform.position, target.transform.position) > aggroDistance)
        {
			agent.speed = baseSpeed;
            currentState = MinionState.Patrol;
        }
		
		else if (target && Vector3.Distance(transform.position, target.transform.position) < aggroDistance)
        {
			agent.speed = aggroSpeed;
			currentState = MinionState.Aggro;
        }
		
	}
	
	void Patrol()
	{
		agent.isStopped = false;
        agent.SetDestination(patrolposition);
        //anim.SetBool("Attack", false);
        //tempo parado
        if (agent.velocity.magnitude < 0.1f)
        {
            stoppedTime += Time.deltaTime;
        }
        //se for mais q timetowait segundos
        if (stoppedTime> timetowait)
        {
            stoppedTime = 0;
            patrolposition = new Vector3(transform.position.x + Random.Range(-patrolDistance, patrolDistance), transform.position.y, transform.position.z + Random.Range(-patrolDistance, patrolDistance));
        }
        //ditancia do jogador for menor q distancetotrigger
        if (Vector3.Distance(transform.position, target.transform.position) < aggroDistance)
        {
			agent.speed = aggroSpeed;
			currentState = MinionState.Aggro;
        }
	}
	
	void Aggro()
	{
		agent.isStopped = false;
		agent.SetDestination(target.transform.position);
		//anim.SetBool("Attack", false);
		
		//se a distancia dele for  menor q 3 ele ataca
        if (Vector3.Distance(transform.position, target.transform.position) < attackDistance)
        {
            currentState = MinionState.Attack;
        }

        //se a distancia dele for  maior q o trigger ele patrulha de novo 
        if (Vector3.Distance(transform.position, target.transform.position) > disengageDistance)
        {
			agent.speed = baseSpeed;
            currentState = MinionState.Patrol;
        }
	}
	
	void Attack()
	{
		if (Vector3.Distance(transform.position, target.transform.position) > atkStopDistance)
		{
			currentState = MinionState.Aggro;
		}
		
		switch(minionType)
		{
			case "Jumper":
			currentState = MinionState.Jump;
			break;
			
			case "ShooterB":
			if(!Projectile) Projectile = BasicShot;
			currentState = MinionState.Shoot;
			break;
			
			case "ShooterH":
			if(!Projectile) Projectile = HomingShot;
			currentState = MinionState.Shoot;
			break;
			
			case "Flier":
			currentState = MinionState.Flying;
			break;
			
			default:
			break;
		}
	}
	
	void Jump()
	{
		Rotate();
		
		if(!jumping)
		{
			anim.SetTrigger("Jump");
			
			//desativa o agent
			agent.isStopped = true;
			agent.updatePosition = false;
			agent.updateRotation = false;
		
			//ativa o movimento do rigid e faz o minion pular
			rigid.isKinematic = false;
			rigid.AddRelativeForce(JumpForce, ForceMode.Impulse);
			
			//pra evitar bugs
			jumpTimer -= Time.deltaTime;
			if(jumpTimer < 0)
			{
				jumpTimer = maxJT;
				jumping = true;
			}
		}
		
		anim.SetFloat("JumpSpd", rigid.velocity.y);
		
		if(stopJump)//acontece quando a aranha toca no chao pelo AIJumpCollision
		{
			anim.SetTrigger("Jump");
			
			jumping = false;
			stopJump = false;
			currentState = MinionState.Aggro;
		}
	}
	
	void Shoot()
	{
		agent.SetDestination(target.transform.position);
		
		if(atkCD > 0)
			atkCD -= Time.deltaTime;
		else
		{
			GameObject Attack = Instantiate(Projectile,
											transform.position,
											transform.rotation);
			Vector3 spawnPoint = transform.TransformDirection(atkSpawnX, atkSpawnY, atkSpawnZ);
			Attack.transform.position = Attack.transform.position + spawnPoint;
			
			atkCD = maxAtkCD;
			currentState = MinionState.Aggro;
		}
	}
	
	void Flying()
	{
		if(!jumping)
		{
			//desativa o agent
			agent.isStopped = true;
			agent.updatePosition = false;
			agent.updateRotation = false;
			//ativa o movimento do rigid
			rigid.isKinematic = false;
			
			jumping = true;
		}
		
		//faz o minion voar
		transform.LookAt(target.transform.position + new Vector3(0, 1, 0));
		rigid.velocity = transform.forward * aggroSpeed;
		
		if (Vector3.Distance(transform.position, target.transform.position) > disengageDistance)
        {
			jumping = false;
			
			//faz o minion nao se teleportar de volta pro comeco do pulo
			agent.nextPosition = transform.position;
			
			//reativa o agent
			agent.updatePosition = true;
			agent.updateRotation = true;
			
			//desativa o movimento do rigid
			rigid.isKinematic = true;
			
			agent.speed = baseSpeed;
            currentState = MinionState.Patrol;
        }
	}
	
	void Dying()
	{
		agent.isStopped = true;
        //anim.SetBool("Attack", false);
		if(!dead)
		{
			switch(minionType)
			{
				case "Flier":
				if(Drop && !GSD.safira)
				{
					Instantiate(Drop, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
				}
				GSD.safira = true;
				break;
				
				case "ShooterB":
				if(Drop && !GSD.pomo)
				{
					Instantiate(Drop, transform.position, Quaternion.identity);
				}
				GSD.pomo = true;
				break;
				
				case "ShooterH":
				if(Drop && !GSD.guarnicao)
				{
					Instantiate(Drop, transform.position, Quaternion.identity);
				}
				GSD.guarnicao = true;
				break;
				
				case "Jumper":
				if(Drop && !GSD.cabo)
				{
					Instantiate(Drop, transform.position, Quaternion.identity);
				}
				GSD.cabo = true;
				break;
				
				default:
				if(Drop && !GSD.lamina)
				{
					Instantiate(Drop, transform.position, Quaternion.identity);
				}
				GSD.lamina = true;
				break;
			}
			//anim.SetTrigger("Die");
			
			if(Boss) Boss.SendMessage("MinionDied");
			dead = true;
		}
        Destroy(gameObject, deathTimer);
	}
	
	void Rotate()
	{
		Vector3 direction = (target.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 6);
	}
}