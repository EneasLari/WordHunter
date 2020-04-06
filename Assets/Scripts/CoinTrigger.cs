using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour {
	private LettersGame LettersGameObject;
	void Start(){
		gameObject.GetComponent<AudioSource>().playOnAwake=false;
		gameObject.GetComponent<Animator> ().enabled = false;
		
	}
	// Use this for initialization
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag.Equals ("Player")) {
			LettersGameObject = GameObject.FindGameObjectWithTag("GameObject").GetComponent<LettersGame>();
			LettersGameObject.coins++;
			gameObject.GetComponent<Animator> ().enabled = true;
			gameObject.GetComponent<AudioSource>().Play();
			Destroy (gameObject,0.195f);
		}

	}
}
