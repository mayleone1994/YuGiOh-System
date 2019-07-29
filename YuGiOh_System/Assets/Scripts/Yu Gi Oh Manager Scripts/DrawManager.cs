using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuGiOhManager{
	
	public static partial class DrawManager{

		private const float timeToAnimation = 0.3F;

		public static IEnumerator DrawCards(int amount, bool isMainPlayer = true){

			var currentDeck = isMainPlayer ? DeckManager.deck : DeckManager.deckAI;

			for (int i = 0; i < amount; i++) {
				if (currentDeck.Count > 0) {
					yield return new WaitForSeconds (timeToAnimation);
					InstantiateInScene (isMainPlayer);
				} else {
					DialogueUI.ShowDialogue ((isMainPlayer ? "Player 1" : "Player 2") + " doesn't have enough card to draw. Lose!");
					GameManager.gameOver = true;
					yield break;
				}
			} 
		}
	}
}
