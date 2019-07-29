using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {

	void Start(){

		GameManager.optionsObject = this.gameObject;
		this.gameObject.SetActive (false);
	}
}
