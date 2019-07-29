using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOhManager;

public class DebugTestScript : MonoBehaviour {


	void Update () {

		if (Input.GetKey (KeyCode.B)) {
			DeckManager.deck.Clear ();
			HandManager.handCards.Clear ();
			DeckManager.deckAI.Clear ();
			HandManager.handAICards.Clear ();
			MouseManager.OnUIRefresh -= GetComponent<UIManager> ().Refresh;
			MouseManagerAI.OnUIRefresh -= GetComponent<UIManager> ().Refresh;
			GameManager.currentCard = null;
			BoardManager.boardCards.Clear ();
			BoardManager.boardAICards.Clear ();
			GameManager.CurrentPhase = Phases.DrawPhase;
			Application.LoadLevel (0);
		}

		if (Input.GetKeyDown (KeyCode.A)) {

			Debug.Log ("Quantidade de cartas no deck: " + DeckManager.deck.Count);
			Debug.Log ("Quantidade de cartas no deck AI: " + DeckManager.deckAI.Count);
			Debug.Log ("Quantidade de cartas na mão: " + HandManager.handCards.Count);
			Debug.Log ("Quantidade de cartas na mão AI " + HandManager.handAICards.Count);
			Debug.Log ("Quantidade de cartas no tabuleiro: " + BoardManager.boardCards.Count);
			Debug.Log ("Quantidade de cartas no tabuleiro AI: " + BoardManager.boardAICards.Count);
		}
	}
}
