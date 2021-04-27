using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqBotaoFinish : MonoBehaviour
{
	[SerializeField]
	SeqBotao SQ1, SQ2, SQ3;
	
	[SerializeField]
	int acertos;
	[SerializeField]
	bool finish;
	
	[SerializeField]
	GameObject ScndKey;

    // Update is called once per frame
    void Update()
    {
        if(!finish)
		{
			acertos = SQ1.acertou + SQ2.acertou + SQ3.acertou;
			if(acertos >= 3)
			{
				Instantiate(ScndKey, transform.position, Quaternion.identity);
				finish = true;
			}
		}
		
    }
}
