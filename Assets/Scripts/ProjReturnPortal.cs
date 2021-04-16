using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjReturnPortal : MonoBehaviour
{
	public GameSessionData GSD;
	
	public Vector3 spawnLocation;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			GSD.playerPosition = spawnLocation;
			
            StartCoroutine(MyLoadScene());
        }
    }
    IEnumerator MyLoadScene()
    {
        Camera.main.SendMessage("FadeOut");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("ProjMainScene");
    }
}
