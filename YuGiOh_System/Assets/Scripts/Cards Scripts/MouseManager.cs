using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using YuGiOhManager;

public class MouseManager : MonoBehaviour {

	protected Card _card;
	protected SpriteRenderer _sprite;

	public static event OnRefreshHandler OnUIRefresh;


	protected void Start(){

		_card = GetComponent<Card> ();
		_sprite = GetComponent<SpriteRenderer> ();
		_sprite.color = Color.grey;
	}

	protected virtual void OnMouseOver(){

		if (!GameManager.isDialogueActived) 
			OnUIRefresh (_card.data.Name, _card.data.Atk, _card.data.Def, _card.spriteUI);

		if (!_card.battled)
			_sprite.color = Color.white;

		}
		

	public void OnMouseExit(){
		OnUIRefresh ("", 0, 0, _card._verseCard);
		_sprite.color = Color.grey;

	}
}
