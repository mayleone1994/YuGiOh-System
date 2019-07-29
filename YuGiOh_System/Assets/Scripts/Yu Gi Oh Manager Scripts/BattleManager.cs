using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuGiOhManager{
	public static class BattleManager {

		public static GameObject attacking, attacked, prefabAnimation;
		public static int attackingPoints, attackedPoints;

		private static int decreasePoints;

		private static BattleAnimationManager animation;

		static BattleManager(){

			prefabAnimation = Resources.Load<GameObject> ("Prefabs/Explosion Animation");
			animation = GameObject.FindObjectOfType<BattleAnimationManager> ();
		}

		public static void AttackLifePointsDirectly(int atkPoints){

			animation.StartAnimation (atkPoints);

			if (GameManager.mainPlayerTurn)
				GameManager.AILifePoints -= atkPoints;
			else
				GameManager.playerLifePoints -= atkPoints;

			EndBattle ();
		}

		public static void StartBattle(){

			StartComponents ();
			DefinePoints ();
			CheckBattleType(AttackedCardIsOnDefense());
		}

		private static void StartComponents(){

			if (GameManager.mainPlayerTurn) {
				attacking = GameManager.currentCard;
				attacked = GameManager.currentCardAI;
			} else {

				attacking = GameManager.currentCardAI;
				attacked = GameManager.currentCard;
			}
				
		}

		private static void DefinePoints(){

			attackingPoints = attacking.GetComponent<Card> ().data.Atk;

			attackedPoints = AttackedCardIsOnDefense () 
				? attacked.GetComponent<Card> ().data.Def
				: attacked.GetComponent<Card> ().data.Atk;

		}

		private static bool AttackedCardIsOnDefense(){

			return attacked.GetComponent<Card> ().isOnDefense;
		}

		private static void CheckBattleType(bool isOnDefenseMode){

			if (isOnDefenseMode)
				AttackVersusDefense ();
			else
				AttackVersusAttack ();
		}

		private static void AttackVersusDefense(){

			if (attackingPoints > attackedPoints) {

				animation.StartAnimation (AnimationsType.AttackingWinsAttacked);
				DestroyAttacked (attacked);

			} else if (attackingPoints < attackedPoints) {
				
				decreasePoints = DecreaseLifePointsAttackingLost (attackedPoints, attackingPoints);
				animation.StartAnimation (AnimationsType.AttackingFailsAttacked, decreasePoints);

			} else {
				
				animation.StartAnimation (AnimationsType.None);
			} 

			EndBattle ();
		}

		private static void AttackVersusAttack(){

			if (attackingPoints > attackedPoints) {

				decreasePoints = DecreaseLifePointsAttackedLost (attackingPoints, attackedPoints);
				animation.StartAnimation (AnimationsType.AttackingWinsAttacked, decreasePoints);
				DestroyAttacked (attacked);

			} else if (attackingPoints < attackedPoints) {
				
				decreasePoints = DecreaseLifePointsAttackingLost (attackedPoints, attackingPoints);
				animation.StartAnimation (AnimationsType.AttackingLostAttacked, decreasePoints);
				DestroyAttacking (attacking);
			} else {

				animation.StartAnimation (AnimationsType.AttackingAndAttackedLost);
				DestroyBothLosers ();
			}

			EndBattle ();
		}

		private static int DecreaseLifePointsAttackedLost(int winner, int loser){

			var diference = winner - loser;


				if (GameManager.mainPlayerTurn)
					GameManager.AILifePoints -= diference;
				else
					GameManager.playerLifePoints -= diference;

			return diference;
		}

		private static int DecreaseLifePointsAttackingLost(int winner, int loser){

			var diference = winner - loser;

			if (GameManager.mainPlayerTurn)
				GameManager.playerLifePoints -= diference;
			else
				GameManager.AILifePoints -= diference;

			return diference;
		}

		private static void DestroyAttacked(GameObject loser){

			if (GameManager.mainPlayerTurn)
				BoardManager.boardAICards.Remove (loser);
			else
				BoardManager.boardCards.Remove (loser);

			DestroyFromScene (loser);
		}

		private static void DestroyAttacking(GameObject loser){

			if (GameManager.mainPlayerTurn)
				BoardManager.boardCards.Remove (loser);
			else
				BoardManager.boardAICards.Remove (loser);

			DestroyFromScene (loser);
		}

		private static void DestroyBothLosers(){

			BoardManager.boardAICards.Remove (GameManager.currentCardAI);
			BoardManager.boardCards.Remove (GameManager.currentCard);
			DestroyFromScene (attacked);
			DestroyFromScene (attacking);
		}

		public static void DestroyFromScene(GameObject destroyed){

			MonoBehaviour.Instantiate (prefabAnimation, destroyed.transform.position, Quaternion.identity);
			destroyed.GetComponent<MouseManager> ().OnMouseExit ();
			MonoBehaviour.Destroy (destroyed);
		}

		private static void EndBattle(){
			
			GameManager.CurrentPhase = Phases.MainPhase;
			GameManager.currentCard = null;
			GameManager.currentCardAI = null;
		}
	}
}
