using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOhManager;
using System.Linq;
using System;

public class AIDecision_HandToBoard : MonoBehaviour {

	public float timeToSelectHand;

	private bool selectedCard = false;

	private List<GameObject> boardPlayer = new List<GameObject> ();
	private List<GameObject> myBoard = new List<GameObject> ();
	private List<GameObject> myHand = new List<GameObject> ();

	public IEnumerator DecisionHandToBoard(){

		yield return new WaitForSeconds (timeToSelectHand);

		InitializeDecision ();
	}

	private void InitializeDecision(){

		selectedCard = false;

		if (BoardManager.IsBoardEmpty (BoardManager.boardCards)) {

			SelectPowerFullCard ();
			
		} else
			Decision ();
	}

	private void SelectPowerFullCard(){

		myHand = OrderListByDescAttack (HandManager.handAICards);

		SelectCardToBoard (myHand [0]);
	}
		

	private void Decision(){

		boardPlayer = OrderListByDescAttack (BoardManager.boardCards);

		for (int i = 0; i < BoardManager.boardCards.Count; i++) {

			if (selectedCard)
				break;

			if (BoardManager.boardCards [i].GetComponent<Card> ().isOnDefense)
				CheckDefenseCard (i);
			else
				CheckAttackCard (i);
		}

		if (!selectedCard) {
			if (BoardManager.boardCards.Count > 0 && BoardManager.boardAICards.Count > 0)
				CheckMyOtherCard ();
			else
				TryDefense ();
		}
	}
		
	private void CheckAttackCard(int i){

		DefineOrderHand ();
		boardPlayer = OrderListByDescAttack (BoardManager.boardCards);

			var cardOfPlayer = boardPlayer [i];

			var card = myHand.FirstOrDefault (c => 
			c.GetComponent<Card> ().data.Atk >= cardOfPlayer.GetComponent<Card> ().data.Atk &&
			          CardDefenseIsLowerThanAttack (c));

			if (card != null) 
				SelectCardToBoard (card);

	}
		
	private void CheckDefenseCard(int i){

		DefineOrderHand ();

		boardPlayer = OrderListByDescAttack (BoardManager.boardCards);

		var cardOfPlayer = boardPlayer [i];

		var card = myHand.FirstOrDefault (c => 
			c.GetComponent<Card> ().data.Atk > cardOfPlayer.GetComponent<Card> ().data.Def &&
			CardDefenseIsLowerThanAttack (c));

		if (card != null) 
			SelectCardToBoard (card);
	}

	private void CheckMyOtherCard(){

		var myBoard = OrderListByDescAttack (BoardManager.boardAICards);

		var controlAttackOrDefense = boardPlayer.FirstOrDefault (b =>
			myBoard [0].GetComponent<Card> ().data.Atk >= b.GetComponent<Card> ().data.Atk);

		if (controlAttackOrDefense != null)
			SelectPowerFullCard ();
		else
			TryDefense ();
	}

	private void TryDefense(){

		boardPlayer = OrderListByDescAttack (BoardManager.boardCards);
		myHand = OrderListByCresDefense (HandManager.handAICards);

		foreach (var cardOfPlayer in boardPlayer) {

			var cardToSelect = myHand.FirstOrDefault(m => m.GetComponent<Card>().data.Def >= cardOfPlayer.GetComponent<Card>().data.Atk
				&& !cardOfPlayer.GetComponent<Card>().isOnDefense);

			if(cardToSelect != null){

				SelectCardToBoard(cardToSelect);
				break;
			}
		}

		if(!selectedCard)
			DiscardCard();
	}

	private void DiscardCard(){

		myHand = OrderListByCresAttack (HandManager.handAICards);

		SelectCardToBoard (myHand [0]);
	}

	private List<GameObject> OrderListByDescAttack(List<GameObject> list){

		return list.OrderByDescending (l => l.GetComponent<Card>().data.Atk).ToList ();
	}


	private List<GameObject> OrderListByCresAttack(List<GameObject> list){

		return list.OrderBy(l => l.GetComponent<Card>().data.Atk).ToList ();
	}

	private List<GameObject> OrderListByDescDefense(List<GameObject> list){

		return list.OrderByDescending (l => l.GetComponent<Card>().data.Def).ToList ();
	}

	private List<GameObject> OrderListByCresDefense(List<GameObject> list){

		return list.OrderBy (l => l.GetComponent<Card>().data.Def).ToList ();
	}

	private void DefineOrderHand(){

		if (BoardManager.boardCards.Count == 1) 
			myHand = OrderListByCresAttack (HandManager.handAICards);
		 else 
			myHand = OrderListByDescAttack(HandManager.handAICards);

		
	}

	private bool CardDefenseIsLowerThanAttack(GameObject card){

		return card.GetComponent<Card> ().data.Atk >= card.GetComponent<Card> ().data.Def;
	}

	private void SelectCardToBoard(GameObject card){

		GameManager.currentCard = card;
		card.GetComponent<CardsActionsAI> ().SetDown ();
		selectedCard = true;

		StartCoroutine (GetComponent<AI>().Decision ());
	}
}
