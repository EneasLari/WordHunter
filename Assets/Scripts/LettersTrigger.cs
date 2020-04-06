using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersTrigger : MonoBehaviour {

	private LettersGame LettersgameGameobject;
    private TilesInitialization TilesInitializationObject;
	void Start(){
		gameObject.GetComponent<AudioSource>().playOnAwake=false;
		gameObject.GetComponent<Animator> ().enabled = false;
		LettersgameGameobject = GameObject.FindGameObjectWithTag("GameObject").GetComponent<LettersGame>();
		TilesInitializationObject = GameObject.FindGameObjectWithTag("TilesInitializationGameObject").GetComponent<TilesInitialization>();

	}


	// Update is called once per frame
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag.Equals("Player")) {
			gameObject.GetComponent<Animator>().enabled = true;
			string result = LettersgameGameobject.getNextCharacter(gameObject.name);
			if (result.Equals("CorrectLetter")) {
				LettersgameGameobject.score++;
				gameObject.GetComponent<AudioSource>().Play();
				print("SHOULD PLAY");
				col.gameObject.GetComponent<PlayerMovement>().speed += .08f;
			} else if (result.Equals("Wrong")) {
				col.gameObject.GetComponent<PlayerMovement>().speed += .4f;
				LettersgameGameobject.lifes--;
			} else if (result.Equals("WordFinished")) {
				LettersgameGameobject.score += 10;
				LettersgameGameobject.lifes++;
				LettersgameGameobject.getNextWord();
				LettersgameGameobject.letterCollected = "";
			}
			Destroy(gameObject, 0.195f);
		}
	}
}
