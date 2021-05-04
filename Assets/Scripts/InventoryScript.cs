using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
	[SerializeField]
	GameObject Boot, Key1, Key2, Helm, Carrot, Potion, Map, Shield, MapImg;
	[SerializeField]
	Text txt;
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
		if(GSD.hasCarrot)
			EnableCarrot();
		if(GSD.hasPotion)
			EnablePotion();
		if(GSD.hasShield)
			EnableShield();
		if(GSD.hasMap)
			EnableMap();
    }
	
	void Update()
	{
		txt.text = GSD.coins.ToString();
		
		if (Input.GetKeyDown(KeyCode.M) && GSD.hasMap)
		{
			Map.SetActive(!Map.activeSelf);
		}
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
	public void EnableCarrot()
	{
		Carrot.SetActive(true);
	}
	public void EnablePotion()
	{
		Potion.SetActive(true);
	}
	public void EnableShield()
	{
		Shield.SetActive(true);
	}
	public void EnableMap()
	{
		MapImg.SetActive(true);
	}
}
