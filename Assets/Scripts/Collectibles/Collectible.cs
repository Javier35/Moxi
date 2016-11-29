using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class Collectible : MonoBehaviour {

	public LevelManager levelManager;
	public string collectibleType = "";

	public void SetLevelManager(){
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
	}
}
