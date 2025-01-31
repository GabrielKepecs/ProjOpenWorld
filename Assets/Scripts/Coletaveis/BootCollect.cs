﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootCollect : aCollectable
{
	[SerializeField]
	InventoryScript IS;
	
    // Start is called before the first frame update
    public override void Start()
    {
        if(GSD.hasBoots)
		{
			Destroy(gameObject);
		}
    }
	
	public override void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			GSD.hasBoots = true;
			TrdC.mayFly = true;
			IS.EnableBoot();
			
			Destroy(gameObject);
		}
	}
}
