using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaveCollect : aCollectable
{
	[SerializeField]
	int keyNumber;
	
    // Start is called before the first frame update
    public override void Start()
    {
        if(keyNumber == 0 && GSD.hasTargetKey || keyNumber == 1 && GSD.hasSecondKey)
			Destroy(gameObject);
    }

    public override void OnTriggerEnter(Collider other)
	{
        if(other.gameObject.tag == "Player")
		{
			if(keyNumber == 0)
			{
				GSD.hasTargetKey = true;
			}
			else
			{
				GSD.hasSecondKey = true;
			}
			
			Destroy(gameObject);
		}
    }
}
