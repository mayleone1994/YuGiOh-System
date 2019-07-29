using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YuGiOhManager;

public class UIManager : MonoBehaviour {

	public Text nameText, atkText, defText;
	public Image image;

	private string mainTextName, mainTextAtk, mainTextDef;

	void Start(){

		MouseManager.OnUIRefresh += Refresh;
		MouseManagerAI.OnUIRefresh += Refresh;

		mainTextName = nameText.text;
		mainTextAtk = atkText.text;
		mainTextDef = defText.text;
	}

	public void Refresh(string name, int atk, int def, Sprite spr ){

			if (GameManager.currentCard == null || (GameManager.currentCard != null &&
			(GameManager.waitingDecision || GameManager.CurrentPhase == Phases.BattlePhase)) 
			|| ((GameManager.currentCard != null && GameManager.CurrentPhase == Phases.MainPhase && !GameManager.mainPlayerTurn))) {

				nameText.text = mainTextName + " " + name;
				atkText.text = mainTextAtk + " " + atk.ToString ();
				defText.text = mainTextDef + " " + def.ToString ();
				image.sprite = spr;
			}
		}
}
