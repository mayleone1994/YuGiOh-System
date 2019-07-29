using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOhManager;
using System.Linq;

public class AI : MonoBehaviour {

	public float timeToLoop;
	public float timeToEndPhase;

	public static event OnForceModeHandler OnForceMode;

	private bool updateDecision = false;

	private List<GameObject> myBoard = new List<GameObject>();


	public IEnumerator Decision (){

		myBoard = BoardManager.boardAICards.OrderBy (
			b => b.GetComponent<Card> ().data.Atk).ToList ();

		if (BoardManager.boardCards.Count > 1)
			CheckOrderCard ();

		Label:
		var index = 0;
		updateDecision = false;
		StopCoroutine (Decision ());

		while (index != myBoard.Count && !GameManager.gameOver){
				
			yield return new WaitForSeconds (timeToLoop);
			CanAttackVersusAttack (myBoard [index]);

			if (updateDecision) {
				yield return new WaitForSeconds (timeToLoop);
				goto Label;
			}

			index++;
		} 
			
		OnForceMode ();
		StartCoroutine (EndTurn ());
	}

	private void CheckOrderCard(){

		myBoard = BoardManager.boardAICards.OrderByDescending (m => m.GetComponent<Card> ().data.Atk).ToList ();
		var myPowerFullCard = myBoard [0];
		var boardPlayer = BoardManager.boardCards.OrderByDescending (b => b.GetComponent<Card> ().data.Atk).ToList ();
		var powerFullCardPlayer = boardPlayer [0];

		if (myPowerFullCard.GetComponent<Card> ().data.Atk <
		   powerFullCardPlayer.GetComponent<Card> ().data.Atk) {

			myBoard = BoardManager.boardAICards.OrderByDescending (m => m.GetComponent<Card> ().data.Atk).ToList ();
		} else 
			BoardManager.boardAICards.OrderBy (m => m.GetComponent<Card> ().data.Atk).ToList ();
	}
		

	private void CanAttackVersusAttack(GameObject myCard){

		if (myCard != null) {

			List<GameObject> cardsOnAttack = new List<GameObject> ();
			cardsOnAttack.Clear ();

			cardsOnAttack = BoardManager.boardCards.Where (
				c => c.GetComponent<Card> ().isOnDefense == false).OrderByDescending (
				c => c.GetComponent<Card> ().data.Atk).ToList ();

			var cardToAttack = cardsOnAttack.FirstOrDefault (
				                  c => c.GetComponent<Card> ().data.Atk <= myCard.GetComponent<CardAI> ().data.Atk);

				if (CanAttack (myCard) && cardToAttack != null) {

					Attack (myCard, cardToAttack);
				} else
					CanAttackVersusDefense (myCard);
					 
		}
	}

	private void CanAttackVersusDefense(GameObject myCard){

		if (myCard != null) {

			List<GameObject> cardsOnDefense = new List<GameObject> ();
			cardsOnDefense.Clear ();

			cardsOnDefense = BoardManager.boardCards.Where (
				c => c.GetComponent<Card> ().isOnDefense == true).OrderByDescending (
				c => c.GetComponent<Card> ().data.Def).ToList ();

			var cardToAttack = cardsOnDefense.FirstOrDefault (
				                  c => c.GetComponent<Card> ().data.Def < myCard.GetComponent<CardAI> ().data.Atk);

			if (CanAttack (myCard) && cardToAttack != null) {

				Attack (myCard, cardToAttack);

			} else 
				AttackLifePoints(myCard);

		}
 	}

	private void Attack(GameObject myCard, GameObject cardToAttack){

		if (myCard.GetComponent<CardAI> ().isOnDefense)
			myCard.GetComponent<CardsActionsAI> ().ChangeMode ();

		GameManager.currentCard = cardToAttack;
		myCard.GetComponent<CardsActionsAI> ().Battle ();

		updateDecision = true;
	}

	private bool CanAttack(GameObject myCard){

		return (myCard != null && !myCard.GetComponent<Card> ().battled
			&& myCard.GetComponent<Card> ().data.Atk >= myCard.GetComponent<Card> ().data.Def);
	}

	private bool AttackLowerDefense(GameObject myCard){

		return myCard.GetComponent<Card> ().data.Atk >= myCard.GetComponent<Card> ().data.Def &&
		(myCard.GetComponent<Card> ().data.Def - myCard.GetComponent<Card> ().data.Atk >= 100 &&
		myCard.GetComponent<Card> ().data.Def - myCard.GetComponent<Card> ().data.Atk <= 350) &&
		myCard.GetComponent<Card> ().data.Atk > 1400;
	}


	private void AttackLifePoints(GameObject myCard){

		if (BoardManager.IsBoardEmpty (BoardManager.boardCards) && CanAttack (myCard)) 

			AttackDirectly (myCard);
		
	}

	private void AttackDirectly(GameObject myCard){

		if (myCard.GetComponent<CardAI> ().isOnDefense)
			myCard.GetComponent<CardsActionsAI> ().ChangeMode ();
		
		myCard.GetComponent<CardsActionsAI> ().AttackingDireclty ();
		}

	public void SelectCardToReplace(){

		if (GameManager.waitingDecision) {

			myBoard = BoardManager.boardAICards.OrderBy (m => m.GetComponent<Card> ().data.Atk).ThenBy (m =>
				m.GetComponent<Card> ().data.Def).ToList ();

			BoardManager.ReplaceCard (myBoard[0].transform);
		}
	}

	public IEnumerator EndTurn(){

		yield return new WaitForSeconds (timeToEndPhase);
		ChangeTurnManager.EndPhase ();
	}
}
