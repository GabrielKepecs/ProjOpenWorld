using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
	[SerializeField]
	GameObject Boot, Key1, Key2, Helm, Carrot, Potion, Map, Shield, MapImg, safira, pomo, lamina, guarni, cabo;
	[SerializeField]
	Text txt;
	[SerializeField]
	public GameSessionData GSD;
	
    // Start is called before the first frame update
    void Awake()
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
		
		GSD.swordPieces = 0;
		if(GSD.safira)
		{
			EnableSafira();
			GSD.swordPieces++;
		}
		if(GSD.pomo)
		{
			EnablePomo();
			GSD.swordPieces++;
		}
		if(GSD.lamina)
		{
			EnableLamina();
			GSD.swordPieces++;
		}
		if(GSD.guarnicao)
		{
			EnableGuarni();
			GSD.swordPieces++;
		}
		if(GSD.cabo)
		{
			EnableCabo();
			GSD.swordPieces++;
		}
		if(GSD.swordPieces >= 5)
			GSD.hasSword = true;
		else
			GSD.hasSword = false;
		
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
	public void EnableSafira()
	{
		safira.SetActive(true);
	}
	public void EnablePomo()
	{
		pomo.SetActive(true);
	}
	public void EnableLamina()
	{
		lamina.SetActive(true);
	}
	public void EnableGuarni()
	{
		guarni.SetActive(true);
	}
	public void EnableCabo()
	{
		cabo.SetActive(true);
	}
}
