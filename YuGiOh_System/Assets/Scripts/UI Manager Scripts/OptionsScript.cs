using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YuGiOhManager;

public class OptionsScript : MonoBehaviour {


	public string myTextDrawPhase, myTextMainPhase;

	private string _text;

	void Update () {

		_text = GameManager.CurrentPhase == Phases.DrawPhase 
			? myTextDrawPhase
			: myTextMainPhase;

		GetComponentInChildren<Text> ().text = _text;

	}

	public void OnClick(){

		GameManager.currentCard.gameObject.GetComponent<CardsActions> ().CallAction (GetComponentInChildren<Text> ().text);
	}
}
