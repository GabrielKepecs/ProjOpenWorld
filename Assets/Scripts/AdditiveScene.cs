using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveScene : MonoBehaviour
{
	[SerializeField]
	string sceneName;
	
    private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			SceneManager.UnloadSceneAsync(sceneName);
		}
	}
}
