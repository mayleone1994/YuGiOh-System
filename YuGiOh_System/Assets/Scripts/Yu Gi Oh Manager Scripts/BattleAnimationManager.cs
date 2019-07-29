using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YuGiOhManager{

	public class BattleAnimationManager : MonoBehaviour{

		public Image player1, player2;
		public Text decreasePlayer1, decreasePlayer2;

		private Text currentText;
		private string trigger;
		private Animator animator;
		private AnimationsType animationType;
		private Vector3 positionText, positionImage1, positionImage2;

		void Start(){

			animator = GameManager.panelAnimation.GetComponent<Animator> ();
			positionText = decreasePlayer2.rectTransform.position;
			positionImage1 = player1.rectTransform.position;
			positionImage2 = player2.rectTransform.position;
		}

		private void StartComponents(){

			player1.gameObject.SetActive (true);
			player2.gameObject.SetActive (true);
			player1.rectTransform.position = positionImage1;
			player2.rectTransform.position = positionImage2;
			decreasePlayer2.rectTransform.position = positionText;
			GameManager.animationBattleRunning = true;
			GameManager.panelAnimation.gameObject.SetActive (true);
		}



		private void GetImages(){

			player1.sprite = GameManager.currentCard.GetComponent<Card> ().spriteUI;
			player2.sprite = GameManager.currentCardAI.GetComponent<CardAI> ().spriteUI;
			player2.gameObject.SetActive (true);
		}

		public void StartAnimation(int decreasePoints = 0){

			StartComponents ();

			player1.sprite = GameManager.currentCard.GetComponent<Card> ().spriteUI;
				
			
			decreasePlayer2.text = "-" + decreasePoints.ToString ();

			animator.SetTrigger ("Attacking_Directly");
		}


		public void StartAnimation(AnimationsType animationType, int decresePoints = 0){

			GetImages ();
			StartComponents ();

				switch (animationType) {

			case AnimationsType.AttackingAndAttackedLost:
				trigger = "BothLose";
				break;

			case AnimationsType.None:
				trigger = "None";
				break;

			case AnimationsType.AttackingWinsAttacked:
				trigger = GameManager.mainPlayerTurn ? "Player1_Wins" : "Player2_Wins";
				currentText = GameManager.mainPlayerTurn ? decreasePlayer2 : decreasePlayer1;
				currentText.text = "-" + decresePoints.ToString ();
					break;

				case AnimationsType.AttackingFailsAttacked:
				trigger = GameManager.mainPlayerTurn ? "Player1_Lose_Defense" : "Player2_Lose_Defense";
				currentText = GameManager.mainPlayerTurn ? decreasePlayer1 : decreasePlayer2;
				currentText.text = "-" + decresePoints.ToString ();
					break;

				case AnimationsType.AttackingLostAttacked:
					trigger = GameManager.mainPlayerTurn ? "Player1_Lose_Attack" : "Player2_Lose_Attack";
				currentText = GameManager.mainPlayerTurn ? decreasePlayer1 : decreasePlayer2;
				currentText.text = "-" + decresePoints.ToString ();
					break;
				}
				
			animator.SetTrigger (trigger);

		}
			

		public static void EndAnimation(){

			GameManager.animationBattleRunning = false;
			GameManager.panelAnimation.gameObject.SetActive (false);
		}

	}

}
