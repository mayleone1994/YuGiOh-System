using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAnimation : MonoBehaviour {

	SlideAnimation instance;

	public float timeToLerp = 12.5F;

	private Vector3 destination;

	void Start(){

		instance = this;
		destination = GetComponent<Card> ().targetPosition;
	}

	void Update(){

		transform.position = Vector3.MoveTowards (transform.position, destination, timeToLerp*Time.deltaTime);

		if (destination == this.transform.position) {
			gameObject.AddComponent<BoxCollider2D> ();
			Destroy (instance);
		}
	}
}
