using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAI : Card {


	protected override void InitializeInformations ()
	{

		base.InitializeInformations ();
		GetComponent<SpriteRenderer>().sprite = _verseCard;
		onDown = true;
	}
}
