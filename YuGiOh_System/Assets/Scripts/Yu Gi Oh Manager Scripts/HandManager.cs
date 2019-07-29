using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuGiOhManager{
	
	public static class HandManager  {

		public static List<GameObject> handCards = new List<GameObject> ();
		public static List<GameObject> handAICards = new List<GameObject> ();

		private static List<GameObject> handList = new List<GameObject> ();

		private const float timeToSortHand = 0.15F;

		private const float handInitialPosition_X = 3.67F;
		private const float cardsSize_X = 0.9F;

		public static IEnumerator SortHand(){

			handList = CheckHandPlayer (GameManager.mainPlayerTurn);

			for (int i = 0; i < handList.Count; i++) {

				yield return new WaitForSeconds (0.1F);


				if (handList [0].transform.position.x != handInitialPosition_X)
				handList [0].transform.position = new Vector3 (handInitialPosition_X, handList [0].transform.position.y, 0);

			if(i > 0)
					handList[i].transform.position = new Vector3(
						(cardsSize_X + handList[i-1].transform.position.x),handList [i].transform.position.y, 0);
			}
		}

		public static List<GameObject> CheckHandPlayer(bool isMainPlayer){

		return isMainPlayer ? handCards : handAICards;
		}

		public static void RemoveFromHandAddToBoard(GameObject _object, bool isMainPlayer = true){

			if (isMainPlayer) {

				handCards.Remove (_object);
				BoardManager.boardCards.Add (_object);
			} else {

				handAICards.Remove (_object);
				BoardManager.boardAICards.Add (_object);
			}
		}
	}
}
