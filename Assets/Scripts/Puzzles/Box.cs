using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	[SerializeField]
	Transform playerTransf;
	
	public int pathDirection;
	
	public float boxSpeed = 5;
	[SerializeField]
	float totalSpeed;
	
	[SerializeField]
	bool pushed;
	
	[SerializeField]
	Vector3 MoveDirection;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		totalSpeed = boxSpeed * Time.deltaTime;
		
        switch(pathDirection)
		{
			case 0:
			if(playerTransf.position.x > transform.position.x + 0.7f && pushed)
				MoveDirection = new Vector3(-1 * totalSpeed, 0, 0);
			break;
			
			case 1:
			if(playerTransf.position.z > transform.position.z + 0.7f && pushed)
				MoveDirection = new Vector3(0, 0, -1 * totalSpeed);
			break;
			
			case 2:
			if(playerTransf.position.x < transform.position.x - 0.7f && pushed)
				MoveDirection = new Vector3(1 * totalSpeed, 0, 0);
			break;
			
			default:
			break;
		}
		if(!pushed)
			MoveDirection = new Vector3(0, 0, 0);
		transform.Translate(MoveDirection);
    }
	
	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
			pushed = true;
	}
	
	private void OnCollisionExit(Collision other)
	{
		if(other.gameObject.tag == "Player")
			pushed = false;
	}
}
