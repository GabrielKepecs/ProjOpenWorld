using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
	[SerializeField]
	GameObject Boot, Key1, Key2, Helm;
	[SerializeField]
	public GameSessionData GSD;
	
    // Start is called before the first frame update
    void Start()
    {
        if(GSD.hasBoots)
			EnableBoot();
		if(GSD.hasTargetKey)
			EnableKey1();
		if(GSD.hasSecondKey)
			EnableKey2();
		if(GSD.hasHelm)
			EnableHelm();
    }

    public void EnableBoot()
	{
		Boot.SetActive(true);
	}
	public void EnableKey1()
	{
		Key1.SetActive(true);
	}
	public void EnableKey2()
	{
		Key2.SetActive(true);
	}
	public void EnableHelm()
	{
		Helm.SetActive(true);
	}
}
