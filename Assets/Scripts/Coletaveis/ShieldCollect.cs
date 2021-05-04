using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollect : aCollectable
{
    // Start is called before the first frame update
    public override void Start()
    {
        if(GSD.hasShield)
		{
			Destroy(gameObject);
		}
    }

    public override void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Player")
		{
			GSD.hasShield = true;
			
			Destroy(gameObject);
		}
    }
}
