using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desgracadecoelhoparadebugar : MonoBehaviour
{
	[SerializeField]
	Transform AItransf;
	
    // Update is called once per frame
    void Update()
    {
        transform.position = AItransf.position;
    }
}
