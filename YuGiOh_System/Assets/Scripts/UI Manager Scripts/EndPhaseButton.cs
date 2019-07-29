using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YuGiOhManager;

public class EndPhaseButton : MonoBehaviour {

	private Image _mySprite;

	private bool canClick;

	void Start(){

		_mySprite = GetComponent<Image> ();
	}

	void Update(){

		_mySprite.color = (GameManager.CurrentPhase == Phases.MainPhase && GameManager.mainPlayerTurn) ? Color.blue : Color.red;
		GetComponentInChildren<Text> ().text = (GameManager.CurrentPhase == Phases.MainPhase && GameManager.mainPlayerTurn)
			? "End Phase" : " Waiting";

		canClick = GameManager.CurrentPhase == Phases.MainPhase && GameManager.mainPlayerTurn && !GameManager.optionsObject.activeSelf;
	}

	public void OnClick(){

		if (canClick) {

			ChangeTurnManager.EndPhase ();

		} else {

			DialogueUI.ShowDialogue ("You cannot <color=yellow>end phase</color> if isn't your turn or if isn't <color=yellow> <b>Main Phase</b></color>");
		}
	}

}
