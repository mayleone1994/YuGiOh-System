using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePointsUI : MonoBehaviour {

	public Text player_Lps;
	public Text AI_Lps;

	void Update () {

		player_Lps.text = GameManager.playerLifePoints.ToString ();
		AI_Lps.text = GameManager.AILifePoints.ToString ();

		if (GameManager.playerLifePoints <= 0) {

			GameManager.playerLifePoints = 0;
			DialogueUI.ShowDialogue ("<color=red>You Lose!!</color>");
		} 
		if (GameManager.AILifePoints <= 0) {

			GameManager.AILifePoints = 0;
			DialogueUI.ShowDialogue ("<color=yellow>You Win!!</color>");
		} 
	}
		
}
