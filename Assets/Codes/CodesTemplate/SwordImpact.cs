using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordImpact : MonoBehaviour
{

    public AudioSource metal;
    public ParticleSystem spark;
	
	public string user;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
		if(collision.gameObject.tag != user && collision.gameObject.tag != "Water")
		{
			spark.transform.position = collision.contacts[0].point;
			spark.Emit(30);
			print(collision.collider.name);
			metal.Play();
			//collision.collider.SendMessage("ExplosionDamage", SendMessageOptions.DontRequireReceiver);
		}
    }
}
