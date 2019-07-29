using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YuGiOhManager{
	
	public static class ChangeTurnManager {

		public delegate void OnBattledHandled ();
		public static event OnBattledHandled OnRefreshBattled;

		public static void EndPhase(){

			if (!GameManager.gameOver) {

				OnRefreshBattled ();
				GameManager.mainPlayerTurn = !GameManager.mainPlayerTurn;
				GameManager.CurrentPhase = Phases.DrawPhase;
				GameManager.currentCard = GameManager.currentCardAI = null;

				if(!GameManager.gameOver)
				InitializeGame.Draw (1, GameManager.mainPlayerTurn);

				if (GameManager.firstTurn)
					GameManager.firstTurn = false;

				if (!GameManager.mainPlayerTurn)
					InitializeGame.CallAI ();
			}
		}
	}
}
