using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace YuGiOhManager{
	
	public static class BoardManager{

		public static List<GameObject> boardCards = new List<GameObject> ();
		public static List<GameObject> boardAICards = new List<GameObject> ();

		private static List<GameObject> board = new List<GameObject> ();

		private const int limitBoardCards = 5;

		private const float boardWidth = 1.2F;

		private const float firstPlaceOnBoard = 3.67F;

		private static float[] places = new float[limitBoardCards];

		static BoardManager(){

			for (int i = 0; i < places.Length; i++) {

				places [i] = i == 0 
					? firstPlaceOnBoard 
					: firstPlaceOnBoard + (boardWidth * i);
			}
		}

		public static void BoardPosition(GameObject _object){

			board = CheckBoard ();

			if (!IsBoardFull()) {
				
				_object.transform.position = new Vector3 (GetPositionX (), GetPositionY(), 0);

				EndBoardPlace ();

			} else {

				GameManager.waitingDecision = true;

				if (GameManager.mainPlayerTurn) {
					GameManager.optionsObject.SetActive (false);
					DialogueUI.ShowDialogue ("Full board. Choice <b>one</b> card to replace!");
				} else {
					
					InitializeGame.mainAI.GetComponent<AI> ().SelectCardToReplace ();
				}
			}

		}

		public static bool IsBoardEmpty(List<GameObject> board){

			return board.Count == 0;
		}

		private static bool IsBoardFull(){

			return board.Count == limitBoardCards;
		}

		public static void EndBoardPlace(){
			
			var card = GameManager.currentCard;

			card.GetComponent<Card> ().myState = CurrentState.OnBoard;

			GameManager.CurrentPhase = Phases.MainPhase;

			HandManager.RemoveFromHandAddToBoard (card, GameManager.mainPlayerTurn);

			if (GameManager.mainPlayerTurn) {
				card.GetComponent<CardsActions> ().StartSortHand ();
				card.GetComponent<CardsActions> ().Cancel ();
			} else 
				card.GetComponent<CardsActionsAI> ().StartSortHand ();
		}

		public static void ReplaceCard(Transform transformCard){

			GameManager.currentCard.transform.position = transformCard.position;

			if (GameManager.mainPlayerTurn)
				boardCards.Remove (transformCard.gameObject);
			else
				boardAICards.Remove (transformCard.gameObject);

			BattleManager.DestroyFromScene (transformCard.gameObject);
			EndBoardPlace ();
		}

		private static List<GameObject> CheckBoard(){

			return GameManager.mainPlayerTurn ? boardCards : boardAICards;
		}

		private static float GetPositionY(){

			return GameManager.mainPlayerTurn ? 2.34F : 3.84F;
		}

		private static float GetPositionX(){

			var newPosition = 0F;

			var currentPlacesWithCards = board.Select(c => c.transform.position.x).ToList ();

			foreach (var place in places) {

				if (!currentPlacesWithCards.Contains (place)) {

					newPosition = place;
					currentPlacesWithCards.Clear ();
					break;
				}
			}

			return newPosition;
		}
	
	}
}
