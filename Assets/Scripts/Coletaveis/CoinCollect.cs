using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : aCollectable
{
    // Start is called before the first frame update
    public override void Start()
    {
        if(GSD.hasCoin || GSD.hasPotion)
		{
			Destroy(gameObject);
		}
    }

    public override void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Player")
		{
			GSD.hasCoin = true;
			
			Destroy(gameObject);
		}
    }
}
