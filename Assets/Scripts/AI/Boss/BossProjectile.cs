using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
	[SerializeField]
	Rigidbody rigid;
	
	[SerializeField]
	float shootTimer, lifeTime,
	hForce, vForce;
	
	[SerializeField]
	bool shot;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shootTimer > 0)
			shootTimer -= Time.deltaTime;
		else
		{
			lifeTime -= Time.deltaTime;
			if(lifeTime < 0) Destroy(gameObject);
			
			if(!shot)
			{
				rigid.isKinematic = false;
				rigid.AddRelativeForce(new Vector3(0, vForce, hForce), ForceMode.Impulse);
				
				shot = true;
			}
		}
    }
	
	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("PlayerProjectile"))
		{
			Destroy(gameObject);
		}
	}
}
