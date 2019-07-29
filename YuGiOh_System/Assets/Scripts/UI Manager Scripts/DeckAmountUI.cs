using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YuGiOhManager;

public class DeckAmountUI : MonoBehaviour {

	public Text amoutDeckPlayer;
	public Text amoutDeckAI;

	void Update(){

		amoutDeckPlayer.text = DeckManager.deck.Count.ToString ();
		amoutDeckAI.text = DeckManager.deckAI.Count.ToString ();
	}
}
