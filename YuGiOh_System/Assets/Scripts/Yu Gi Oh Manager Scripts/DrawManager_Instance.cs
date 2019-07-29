using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuGiOhManager{

public static partial class DrawManager{

		public static class Position{

			private const float widthCard = 0.9F;
			private const float startPosition_X = 3.67F;
			private const float position_MainPlayer_Y = -0.4F;
			private const float position_AI_Y = 6.55F;

			public static float Get_X(){

				return currentHand.Count == 0 ? startPosition_X
						: startPosition_X + (widthCard * currentHand.Count);
			}

			public static float Get_Y(){

				return isMainPlayer ? position_MainPlayer_Y : position_AI_Y;
			}
		}

	private static Vector3 positionToInstantiate;

	private static GameObject cardPrefab;
	private static GameObject cardAIPrefab;
	private static GameObject cardInstance;

	private static Stack<CardData> currentDeck;
	private static List<GameObject> currentHand;

	private static bool isMainPlayer;

		static DrawManager(){

		cardPrefab = Resources.Load<GameObject> ("Prefabs/Card Prefab");
		cardAIPrefab = Resources.Load<GameObject> ("Prefabs/Card Prefab AI");
	}

	public static void InstantiateInScene(bool mainPlayer){

			isMainPlayer = mainPlayer;

			CheckInformations ();

		cardInstance = MonoBehaviour.Instantiate (
				isMainPlayer ? cardPrefab : cardAIPrefab, positionToInstantiate, Quaternion.identity) as GameObject;

		SetData ();

		currentHand.Add (cardInstance);

	}

	private static void CheckInformations(){

			positionToInstantiate = new Vector3 (2.42F, (isMainPlayer ? 1.31F : 4.02F), 0);
			currentDeck = DeckManager.CheckDeckPlayer (isMainPlayer);
			currentHand = HandManager.CheckHandPlayer (isMainPlayer);
		}

	private static void SetData(){

		var _card = isMainPlayer ? cardInstance.GetComponent<Card>() : cardInstance.GetComponent<CardAI>();

		_card.data = currentDeck.Pop ();
		_card.targetPosition = new Vector3(Position.Get_X(), Position.Get_Y(), 0);
		_card.Initialize ();
	}
}
}
