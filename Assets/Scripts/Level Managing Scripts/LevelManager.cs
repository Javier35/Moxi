﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	private GameObject player;
	public Fading fader;

	public ArrayList enemiesToSpawn  = new ArrayList(); //names of enemies that became inactive
	public ArrayList collectiblesToSpawn = new ArrayList();

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Moxi");
		fader = gameObject.GetComponent<Fading> ();
	}

	public void RespawnPlayer(){

		Inventory.LoseALife ();
		if (checkGameOver()) {
			SceneManager.LoadScene("LT1");
			return;
		}
		//fader.BeginFade (1);
		player.GetComponent<PlayerDamageManager> ().Heal (10);
		player.transform.position = currentCheckpoint.transform.position;
		player.GetComponent<PlayerDamageManager> ().BecomeInvincible (1.5f);
		RespawnEnemies ();
		RespawnCollectibles ();
		fader.BeginFade (-1);
	}

	public void RespawnEnemies(){
		foreach (GameObject enemy in enemiesToSpawn) {
			var enemyObject = (GameObject)enemy;
			enemyObject.GetComponent<EnemyRespawn> ().Respawn ();
		}
		enemiesToSpawn.Clear ();
	}


	//respawns all colectibles except for money!
	public void RespawnCollectibles(){
		foreach (GameObject collectible in collectiblesToSpawn) {
			var collectibleObject = (GameObject)collectible;
			collectibleObject.SetActive (true);
		}
		collectiblesToSpawn.Clear ();
	}

	public bool checkGameOver(){
		if (Inventory.GetLives () <= 0)
			return true;
		return false;
	}
}