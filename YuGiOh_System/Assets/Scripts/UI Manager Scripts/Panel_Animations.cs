using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YuGiOhManager;

public class Panel_Animations : MonoBehaviour {

	public Text decreasePlayer1, decreasePlayer2;

	void Awake(){

		GameManager.panelAnimation = this.gameObject;
		this.gameObject.SetActive (false);
	}

	public void EndEvent(){

		decreasePlayer1.gameObject.SetActive (false);
		decreasePlayer2.gameObject.SetActive (false);
		BattleAnimationManager.EndAnimation ();
	}
}
