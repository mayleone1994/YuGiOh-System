using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour {

	private static Text _text;

	private static string dialogue;

	private static DialogueUI instance;

	void Awake(){

		instance = this;
	}

	void Start(){

		_text = GetComponentInChildren<Text> ();
		SettingActive ();
	}

	public static void ShowDialogue(params string[] sentences){
		
		GameManager.isDialogueActived = true;

		SettingActive ();

		dialogue = "";

		foreach (var word in sentences) {

			dialogue += word;
		}

		_text.text = dialogue;

	}

	public static void SettingActive(){

		instance.gameObject.SetActive (GameManager.isDialogueActived);
	}

	void Update(){

		if (Input.GetMouseButtonDown(0)) {
			GameManager.isDialogueActived = false;
			SettingActive ();
		}
	}
}
