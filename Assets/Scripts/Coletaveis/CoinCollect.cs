using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : aCollectable
{
    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Player")
		{
			GSD.coins++;
			
			Destroy(gameObject);
		}
    }
}
