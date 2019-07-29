using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOhManager;

public class CardsActionsAI : Actions {

	private Card _cardComponent;
	private SpriteRenderer _sprite;

	protected override void Awake(){

		_cardComponent = GetComponent<Card> ();
		_sprite = GetComponent<SpriteRenderer> ();
		ChangeTurnManager.OnRefreshBattled += ResetBattled;
		AI.OnForceMode += ForceChangeMode;
	}

	protected override void OnMouseDown(){

		if(GameManager.mainPlayerTurn && !GameManager.isDialogueActived && !GameManager.animationBattleRunning)
		CheckSelect ();
	}

	public override void SetDown(){

		PlaceOnBoard ();
	}

	public override void SetUp(){

		Reveal ();
		PlaceOnBoard ();
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

	public override void ChangeMode(){

		_cardComponent.isOnDefense = !_cardComponent.isOnDefense;

		transform.Rotate (0, 0, (_cardComponent.isOnDefense ? -90 : 90));
	}

	private void ForceChangeMode(){

		if (!_cardComponent.battled && !_cardComponent.isOnDefense && _cardComponent.myState == CurrentState.OnBoard)
			ChangeMode ();
	}

	public void AttackingDireclty(){

		_cardComponent.GetComponent<Card> ().battled = true;
		GameManager.currentCard = this.gameObject;
		Reveal ();
		BattleManager.AttackLifePointsDirectly (_cardComponent.data.Atk);
	}

	public override void Battle(){

			_cardComponent.GetComponent<Card> ().battled = true;
			GameManager.currentCardAI = this.gameObject;
			Reveal ();
			Invoke ("LetsBattle", 1);
	}
		
	protected override void CheckSelect(){

		if (GameManager.mainPlayerTurn && GameManager.CurrentPhase == Phases.BattlePhase &&
			GameManager.currentCardAI == null) {

			Battle ();
		}
	}

	public void LetsBattle(){

		BattleManager.StartBattle ();
	}

	void OnDestroy (){

		AI.OnForceMode -= ForceChangeMode;
	}
}
