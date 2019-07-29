using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using YuGiOhManager;

public class CardsActions : Actions {
	
	private Card _cardComponent;
	private SpriteRenderer _sprite;

	private Dictionary<string, Action> actions = new Dictionary<string, Action>();

	protected override void Awake(){

		actions.Add ("Set Down", SetDown);
		actions.Add ("Set Up", SetUp);
		actions.Add ("Change Mode", ChangeMode);
		actions.Add ("Battle", Battle);
		actions.Add ("Cancel", Cancel);

		_cardComponent = GetComponent<Card> ();
		_sprite = GetComponent<SpriteRenderer> ();

		ChangeTurnManager.OnRefreshBattled += ResetBattled;
	}

	 protected override void OnMouseDown(){

		if (!GameManager.animationBattleRunning) {
			if (GameManager.mainPlayerTurn && GameManager.CurrentPhase != Phases.BattlePhase)
				CheckSelect ();
			else
				DialogueUI.ShowDialogue ("You cannot select card if isn't your turn or if is <color=yellow> Battle Phase</color>!");
		}
	}
		
	public void CallAction(string key){
		
		actions [key].Invoke ();
	}

	public override void SetDown(){

		_sprite.sprite = _cardComponent._verseCard;
		_cardComponent.onDown = true;
		PlaceOnBoard();
	}

	public override void SetUp(){

		PlaceOnBoard();
	}

	public override void ResetBattled(){

		if (_cardComponent.battled)
			_cardComponent.battled = false;
	}

	public override void Reveal(){

		if (_cardComponent.onDown) {
			_sprite.sprite = _cardComponent._mySprite;
			_cardComponent.onDown = false;
		}
	}
		
	public void Cancel(){

		GameManager.currentCard = null;

		if (GameManager.CurrentPhase == Phases.DrawPhase)
			transform.Translate (0, -0.7F, 0);

		if(GameManager.optionsObject.activeSelf)
		GameManager.optionsObject.gameObject.SetActive (false);

	}

	public override void ChangeMode(){

		if (!_cardComponent.battled) {

			_cardComponent.isOnDefense = !_cardComponent.isOnDefense;
			transform.Rotate (0, 0, (_cardComponent.isOnDefense ? -90 : 90));
		} else {
			
			DialogueUI.ShowDialogue ("You cannot change mode after battle!");
		}

		Cancel ();
	}

	public override void Battle(){

		if (!GameManager.firstTurn) {
			if (_cardComponent.isOnDefense) {
				DialogueUI.ShowDialogue ("You cannot change mode in <color=yellow>defense</color> mode!");
				Cancel ();
			} else {

				Reveal ();
				if (!_cardComponent.battled) {
					GameManager.CurrentPhase = Phases.BattlePhase;
					_cardComponent.battled = true;

					if (BoardManager.IsBoardEmpty (BoardManager.boardAICards)) {
						BattleManager.AttackLifePointsDirectly (_cardComponent.data.Atk);
						Cancel ();
					} else {
						GameManager.optionsObject.gameObject.SetActive (false);
					}
				} else {
					DialogueUI.ShowDialogue ("Cannot attack twice!");
					Cancel ();
				}
			} 
		} else {

			DialogueUI.ShowDialogue ("Cannot attack at <b>first</b> turn!");
			Cancel ();
		}
	}

	protected override void CheckSelect(){

		if (!GameManager.isDialogueActived) {

			if (GameManager.currentCard == null && GameManager.CurrentPhase == Phases.DrawPhase &&
			   _cardComponent.myState == CurrentState.OnHand) {

				transform.Translate (0, 0.7F, 0);
			}

			if (GameManager.currentCard == null &&
			   (GameManager.CurrentPhase == Phases.DrawPhase && _cardComponent.myState == CurrentState.OnHand) ||
			   GameManager.CurrentPhase == Phases.MainPhase && _cardComponent.myState == CurrentState.OnBoard &&
			   !GameManager.optionsObject.activeSelf) {

				GameManager.currentCard = this.gameObject;
				GameManager.optionsObject.gameObject.SetActive (true);
			}

			if ((GameManager.currentCard != null && GameManager.currentCard != this.gameObject)
			   && _cardComponent.myState == CurrentState.OnBoard && !GameManager.optionsObject.gameObject.activeSelf) {

				GameManager.waitingDecision = false;
				BoardManager.ReplaceCard (this.transform);
			}
		}
	}

}
