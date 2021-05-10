using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHomingProjectile : MonoBehaviour
{
	[SerializeField]
	Rigidbody rigid;
	
	public GameObject HomingTarget;
	
	[SerializeField]
	float speed;
	
    // Start is called before the first frame update
    void Start()
    {
        HomingTarget = GameObject.Find("Player Warrior boy");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.LookAt(HomingTarget.transform.position + new Vector3(0, 1, 0));
		
		rigid.velocity = transform.forward * speed;
    }
	
	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("PlayerProjectile"))
		{
			Destroy(gameObject);
		}
	}
}
