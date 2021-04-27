using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class aCollectable : MonoBehaviour
{
	public TrdControl TrdC;
	
	public GameSessionData GSD;
	
	public Vector3 VectRotate;
	public float rotateSpd;
	
    // Start is called before the first frame update
    public abstract void Start();

    // Update is called once per frame
    public void Update()
    {
        transform.Rotate(VectRotate * Time.deltaTime * rotateSpd);
    }
	
	public abstract void OnTriggerEnter(Collider other);
}
