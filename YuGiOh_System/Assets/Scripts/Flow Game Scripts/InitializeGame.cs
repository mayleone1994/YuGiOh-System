using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOhManager;

public class InitializeGame : MonoBehaviour{

	private const int initialHandAmount = 5;

	public static InitializeGame instance;

	public static AIDecision_HandToBoard mainAI;

	void Awake(){

		instance = this;

		mainAI = GameObject.Find ("Artificial Inteligence").GetComponent<AIDecision_HandToBoard>();
	}

	public static void StartGame(){

		StartLifePoints ();
		GameManager.mainPlayerTurn = true;
		GameManager.CurrentPhase = Phases.DrawPhase;
		GameManager.gameOver = false;
		GameManager.firstTurn = true;
		ShuffleDecks ();
		DrawInitialCards ();

	}

	static void StartLifePoints(){

		GameManager.playerLifePoints = 8000;
		GameManager.AILifePoints = 8000;
	}

	static void ShuffleDecks(){

		DeckManager.deck.Shuffle ();
		DeckManager.deckAI.Shuffle ();
	}

	static void DrawInitialCards(){

		instance.StartCoroutine(DrawManager.DrawCards(initialHandAmount));
		instance.StartCoroutine (DrawManager.DrawCards (initialHandAmount-1, false));
	}

	public static void Draw(int amount, bool isMainPlayer){

		if(!GameManager.gameOver)
		instance.StartCoroutine(DrawManager.DrawCards(amount, isMainPlayer));

	}

	public static void CallAI(){

		if(!GameManager.gameOver)
			instance.StartCoroutine (mainAI.DecisionHandToBoard());
	}
}
