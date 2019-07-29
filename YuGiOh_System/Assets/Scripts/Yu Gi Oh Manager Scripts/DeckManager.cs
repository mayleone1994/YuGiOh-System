using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace YuGiOhManager{

	public static class DeckManager {

		public static Stack<CardData> deck = new Stack<CardData> ();
		public static Stack<CardData> deckAI = new Stack<CardData>();

		public static Stack<CardData> CheckDeckPlayer(bool isMainPlayer){

			return isMainPlayer ? deck : deckAI;
		}

		public static void Shuffle(this Stack<CardData> myDeck){

			var rand = new System.Random ();

			var values = myDeck.ToArray ();
			myDeck.Clear ();

			foreach(var value in values.OrderBy(v => rand.Next())){

				myDeck.Push (value);
			}
		}
			
			
	}
}
