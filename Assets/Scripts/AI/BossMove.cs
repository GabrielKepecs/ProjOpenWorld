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
    }
	public BossState currentState;
	
	public float repositionSpeed;
	public float chargeSpeed;
	public Vector3 repositionTarget;
	
	public float chargeAtkDistance;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
