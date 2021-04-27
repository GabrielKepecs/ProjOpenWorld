using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmCollect : aCollectable
{
    // Start is called before the first frame update
    public override void Start()
    {
        if(GSD.hasHelm)
		{
			Destroy(gameObject);
		}
    }

    public override void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Player")
		{
			GSD.hasHelm = true;
			
			Destroy(gameObject);
		}
    }
}
