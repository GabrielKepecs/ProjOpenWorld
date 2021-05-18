using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrdControl : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rdb;
    Vector3 move, rot;
    Animator anim;
	
    public float forcemove=1000;
	
    GameObject dummyCam;
	
    public float drag=4;
	
    public AudioSource scream;
	
    public float jumpForce =500;
    float timeJump = 1;
	public bool mayJump = true;//se o jogador pode ou nao pular
	public bool mayFly;//se o jogador pode ou nao voar
	
	[SerializeField]
	Vector3 initialSpawnLocation;//(315, 15, 400)
	
	[SerializeField]
	GameObject Helmet, Shield, Potion, OriginalSword, NewSword, StopProjectile;
	
	public float moveMod = 1;
	
	public float spellCD, spellTimer;
	public bool swordType;
	
    float ikforce = 0;
    bool grab = false;
	
    FixedJoint grabjoint;
	
    public GameObject projetil;
	
    public enum States
    {
        Walk,
        Attack,
        Idle,
        Dead,
        Damage,
        Jump,
        Spell,
		Spell2,
    }

    public States state;
	
	public GameSessionData GSD;//informacao que e mantida durante uma sessao de jogo

    void Start()
    {
        rdb= GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        /*

        Joint[] joints;
        joints = GetComponentsInChildren<Joint>();
        foreach (Joint myjoint in joints)
        {
            Destroy(myjoint);
        }

        Rigidbody[] rdbs;
        rdbs = GetComponentsInChildren<Rigidbody>();
        for(int i=1;i<rdbs.Length;i++)
        {
            Destroy(rdbs[i]);
        }
        */
        rdb.isKinematic=false;
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            if (CommomValues.ShrinePlayerPosition.magnitude > 0)
            {
                transform.position = CommomValues.ShrinePlayerPosition;
            }
        }
        
        StartCoroutine(Idle());
		
		if(GSD.spawned == false)
		{
			GSD.playerPosition = initialSpawnLocation;
			GSD.spawned = true;
		}
		transform.position = GSD.playerPosition;
		
		if(GSD.hasBoots) mayFly = true;
		if(GSD.hasHelm) EnableHelm();
		if(GSD.hasCarrot) moveMod = 1.3f;
		if(GSD.hasShield) EnableShield();
		
		if(GSD.hasSword) SwitchSword();
    }

    public void SetDummyCam(GameObject dummy)
    {
        dummyCam = dummy;
    }

    // Update is called once per frame
    void Update()
    {
        //criacao de vetor de movimento local
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        if (dummyCam)
            move = dummyCam.transform.TransformDirection(move);

        if (move.magnitude > 0 && !grab)
        {
            transform.forward = Vector3.Slerp(transform.forward,move,Time.deltaTime*10);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ChangeState(States.Attack);
        }
        if (Input.GetButtonDown("Fire2") && spellCD >= spellTimer)
        {
			spellCD = 0;
			
			if(!swordType)
				ChangeState(States.Spell);
			else
				ChangeState(States.Spell2);
        }
		
		if(spellCD < spellTimer)
			spellCD += Time.deltaTime;
		
		mayJump = Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Vector3.down, 0.5f);
		//Debug.DrawLine(transform.position + new Vector3(0, 0.2f, 0), Vector2.down, Color.white, 0.5f);
        if (Input.GetButtonDown("Jump"))
        {
			if(mayJump || mayFly)//se tem algo em baixo do player ou mayFly
			{
				ChangeState(States.Jump);
			}
        }
        if (Input.GetButtonUp("Jump"))
        {
           if(timeJump > 0.1f)
            {
                timeJump = 0.1f;
            }
        }
        grab = Input.GetButton("Fire3");
		
		if(Input.GetKeyDown(KeyCode.LeftShift) && GSD.hasSword)
			SwitchSword();
    }


    private void FixedUpdate()
    {

        float vel = rdb.velocity.magnitude;

        //limite de velocidade
        rdb.AddForce((move * forcemove * moveMod)/ (vel*2+1));
        anim.SetFloat("Velocity", vel);

        //velocidade sem y
        Vector3 velwoy = new Vector3(rdb.velocity.x, 0, rdb.velocity.z);
        //drag manual
        rdb.AddForce(-velwoy * drag);

        if(Physics.Raycast(transform.position+Vector3.up*.5f,Vector3.down,out RaycastHit hit, 511))
        {
            anim.SetFloat("DistGround", hit.distance);
            Debug.DrawLine(transform.position + Vector3.up*.5f, hit.point,Color.red);
        }
        if (!grab)
        {
            ikforce = Mathf.Lerp(ikforce, 0, Time.fixedDeltaTime*3);
        }
    }

    public void ChangeState(States myestate)
    {
        state = myestate;
        StartCoroutine(state.ToString());
    }
    public void Grab()
    {
        if (grab)
        {
            if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out RaycastHit hit, 2, 511))
            {
                if (hit.collider.CompareTag("Push"))
                {
                    ikforce = Mathf.Lerp(ikforce, 1, Time.fixedDeltaTime * 3);
                }
                if (ikforce > 0.9f)
                {
                    if (!grabjoint)
                    {
                        grabjoint = hit.collider.gameObject.AddComponent<FixedJoint>();
                        grabjoint.connectedBody = rdb;
                    }
                }   
            }
        }
        else { 
            if (grabjoint)
            {
                Destroy(grabjoint);
            }
        }
    }

    IEnumerator Idle()
    {
        //entrada
        while (state == States.Idle)
        {
            //loop
            yield return new WaitForFixedUpdate();

            if (rdb.velocity.magnitude > 0.1f)
            {
                ChangeState(States.Walk);
            }
            Grab();
        }
        //saida
    }

    IEnumerator Walk()
    {
        //entrada
        
        while (state == States.Walk)
        {
            //loop
            yield return new WaitForFixedUpdate();
            if (rdb.velocity.magnitude < 0.1f)
            {
                ChangeState(States.Idle);
            }
            Grab();

        }
        //saida
    }

    IEnumerator Attack()
    {
        //entrada
        anim.SetTrigger("Atack");
        scream.Play();
        yield return new WaitForSeconds(2);
        
        ChangeState(States.Idle);
        //saida
    }

    IEnumerator Spell()
    {
        //entrada
        anim.SetTrigger("Spell");
        scream.Play();
        yield return new WaitForSeconds(0.7f);
        GameObject magic = Instantiate(projetil, transform.position + Vector3.up+transform.forward, transform.rotation);
        magic.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);

        yield return new WaitForSeconds(1);
       

        ChangeState(States.Idle);
        //saida
    }
	
	IEnumerator Spell2()
	{
		anim.SetTrigger("Spell");
		scream.Play();
		
		yield return new WaitForSeconds(0.5f);
		StopProjectile.SetActive(true);
		
		yield return new WaitForSeconds(0.5f);
		StopProjectile.SetActive(false);
		
		ChangeState(States.Idle);
	}

    IEnumerator Damage()
    {
        //entrada
        while (state == States.Damage)
        {
            //loop
            yield return new WaitForFixedUpdate();
        }
        //saida
    }

    IEnumerator Dead()
    {
        //entrada
        while (state == States.Dead)
        {
            //loop
            yield return new WaitForFixedUpdate();
        }
        //saida
    }
    IEnumerator Jump()
    {
        timeJump = .4f;
        //entrada
        while (state == States.Jump)
        {
            yield return new WaitForFixedUpdate();
            //loop
			rdb.AddForce(Vector3.up* jumpForce* timeJump);
            timeJump -= Time.fixedDeltaTime;
            if (timeJump <= 0)
            {
                ChangeState(States.Idle);
            }
            
        }
        //saida
    }
	/*//Jump() original
	IEnumerator Jump()
    {
        timeJump = .4f;
        //entrada
        while (state == States.Jump)
        {
            yield return new WaitForFixedUpdate();
            //loop
			rdb.AddForce(Vector3.up* jumpForce* timeJump);
            timeJump -= Time.fixedDeltaTime;
            if (timeJump <= 0)
            {
                ChangeState(States.Idle);
            }
            
        }
        //saida
    }
	*/

    void OnAnimatorIK(int layerIndex)
    {
        

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikforce);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikforce);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, transform.position + Vector3.up + transform.forward - transform.right * 0.5f);
        anim.SetIKPosition(AvatarIKGoal.RightHand, transform.position + Vector3.up + transform.forward + transform.right * 0.5f);

        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikforce);
        anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(-40, 131, 301));//124 307 301

    }
	
	public void EnableHelm()
	{
		Helmet.SetActive(true);
	}
	public void EnableShield()
	{
		Shield.SetActive(true);
	}
	public void EnablePotion()
	{
		Potion.SetActive(true);
	}
	
	public void SwitchSword()
	{
		OriginalSword.SetActive(!OriginalSword.activeSelf);
		NewSword.SetActive(!NewSword.activeSelf);
		
		swordType = !swordType;
		anim.SetBool("SpellType", swordType);
	}
	
	public void GetPushed(float pushStr)
	{
		rdb.AddRelativeForce(Vector3.back * pushStr);
	}
}