using System;

namespace YuGiOhManager{

	public enum CurrentState {OnHand, OnBoard, OnGraveyard};
	public enum Phases {DrawPhase, MainPhase, BattlePhase};

	public enum AnimationsType{AttackingWinsAttacked,
		AttackingFailsAttacked,
		AttackingTiesAttacked,
		AttackingLostAttacked,
		AttackingAndAttackedLost,
		None };

	public delegate void OnRefreshHandler (string name, int atk, int def, UnityEngine.Sprite sprite);
	public delegate void OnForceModeHandler();
}
