using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuGiOhManager;

public class MouseManagerAI : MouseManager {

	public static event OnRefreshHandler OnUIRefresh;

	protected override void OnMouseOver(){

		_sprite.color = Color.white;

		if (!_card.onDown && !GameManager.isDialogueActived) 
			OnUIRefresh (_card.data.Name, _card.data.Atk, _card.data.Def, _card.spriteUI);
	}
}
