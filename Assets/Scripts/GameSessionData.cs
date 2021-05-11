using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GSData", menuName = "ScriptableObjects/GSDataScriptableObject")]

public class GameSessionData : ScriptableObject
{
	public Vector3 playerPosition;
	//default main game: 365, 5.3, 157
	//default shrines: 0, 0, 0
	
	public bool spawned;
	
	public bool hasBoots;
	public bool hasTargetKey;
	public bool hasHelm;
	public bool hasSecondKey;
	public bool hasCarrot;
	public int coins;
	public bool hasPotion;
	public bool hasMap;
	public bool hasShield;
	
	public bool hasSword;
}
