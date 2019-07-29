using System.Collections;
using System.Collections.Generic;
using System;
using YuGiOhManager;

public static class GameManager {

	public static int playerLifePoints;

	public static int AILifePoints;

	public static bool waitingDecision;

	public static bool mainPlayerTurn;

	public static bool firstTurn;

	public static bool isDialogueActived;

	public static bool gameOver;

	public static bool animationBattleRunning;

	public static Phases CurrentPhase;

	public static UnityEngine.GameObject currentCard, currentCardAI;
	public static UnityEngine.GameObject optionsObject, panelAnimation;

}
