using UnityEngine;
using System.Collections;

public class HemelEnding : MonoBehaviour {

	public bool fadeOut = true;
	public float endingSequanceDuration = 6;
	public float soundFadeOutTime = 3;

	void OnTriggerEnter (Collider other) {
		other.gameObject.GetComponent<Player> ().forcedToMove = true;

		StartCoroutine (EndingSequence ());
	}

	IEnumerator EndingSequence () {
		GameObject gameManager = GameObject.Find ("GameManager");

		float durationTimer = 0;
		float soundFadeSpeed = (soundFadeOutTime / .1f) / 1000;

		int fadeDirection = (fadeOut) ? 1 : -1;
		gameManager.GetComponent<ScreenFader> ().BeginFade(fadeDirection);

		while(AudioListener.volume >= soundFadeSpeed) {
			AudioListener.volume -= soundFadeSpeed;
			durationTimer += .1f;
			yield return new WaitForSeconds (.1f);
		}
		AudioListener.volume = 0;

		yield return new WaitForSeconds (endingSequanceDuration - durationTimer);

		Debug.Log ("END OF GAME");
	}

}
