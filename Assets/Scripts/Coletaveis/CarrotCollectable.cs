using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotCollectable : aCollectable
{
    // Start is called before the first frame update
    public override void Start()
    {
        if(GSD.hasCarrot)
		{
			Destroy(gameObject);
		}
    }

    public override void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Player")
		{
			GSD.hasCarrot = true;
			
			Destroy(gameObject);
		}
    }
}
