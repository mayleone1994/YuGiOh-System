using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOhManager;

public class Card : MonoBehaviour{
	
	public CardData data;
	public Vector3 targetPosition;
	public Sprite spriteUI, _verseCard, _mySprite;
	public CurrentState myState;
	public bool battled, isOnDefense, onDown;

	public void Initialize(){

		InitializeInformations();
	}

	protected virtual void InitializeInformations(){

		gameObject.name = data.Name;

		myState = CurrentState.OnHand;

		isOnDefense = battled = false;

		_mySprite = spriteUI = Sprite.Create (data.imageUI, new Rect (0, 0, 200, 280), new Vector2 (0.5f, 0.5f));

		transform.localScale = new Vector3 (0.47F, 0.45F, 0);

		GetComponent<SpriteRenderer> ().sprite = _mySprite;

		onDown = false;
	}
}
