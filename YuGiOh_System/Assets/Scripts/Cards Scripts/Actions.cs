using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOhManager;

public abstract class Actions : MonoBehaviour {

	protected abstract void Awake ();
	protected abstract void OnMouseDown();
	protected abstract void CheckSelect ();

	public abstract void SetUp ();
	public abstract void SetDown ();
	public abstract void Battle ();
	public abstract void ChangeMode ();
	public abstract void ResetBattled ();
	public abstract void Reveal ();

	public void PlaceOnBoard(){

		BoardManager.BoardPosition (GameManager.currentCard);

	}

	public void StartSortHand(){

		StartCoroutine(HandManager.SortHand ());
	}
}
