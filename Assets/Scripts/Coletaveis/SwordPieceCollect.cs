using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPieceCollect : aCollectable
{
	//[SerializeField]
	//int swordNumber;
	//0       1     2       3          4
	//safira, pomo, lamina, guarnicao, cabo
	
    // Start is called before the first frame update
    public override void Start()
    {
        /*switch(swordNumber)
		{
			case 0:
			if(GSD.safira) Destroy(gameObject);
			break;
			
			case 1:
			if(GSD.pomo) Destroy(gameObject);
			break;
			
			case 2:
			if(GSD.lamina) Destroy(gameObject);
			break;
			
			case 3:
			if(GSD.guarnicao) Destroy(gameObject);
			break;
			
			case 4:
			if(GSD.cabo) Destroy(gameObject);
			break;
		}*/
    }

    public override void OnTriggerEnter(Collider other)
	{
		/*if(other.gameObject.tag == "Player")
		{
			//GSD.swordPieces++;
			//if(GSD.swordPieces >= 5)
			//	GSD.hasSword = true;
			
			Destroy(gameObject);
		}*/
	}
}
