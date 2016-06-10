using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour {

	[Tooltip("theDoor is het GameObject dat verplaatst gaat worden.")] 
	public GameObject theDoor;
	[Tooltip("moveAmount is de totale hoeveelheid die het GameObject verplaatst moet worden.")] 
	public float moveAmount = 1.2f;
	[Tooltip("moveSpeed is de hoeveelheid die het GameObject iedere .1 seconde verplaatst tot het zijn eindbestemming bereikt.")] 
	public float moveSpeed = .15f;

	void OnTriggerEnter (Collider other) {
		StartCoroutine (OpenDoor());
	}

	public IEnumerator OpenDoor () {
		if (theDoor.GetComponent <AudioSource> ())
			theDoor.GetComponent <AudioSource> ().Play ();

		while(theDoor.transform.localPosition.x <= moveAmount) {
			Vector3 newPosition = new Vector3 (theDoor.transform.localPosition.x + moveSpeed, theDoor.transform.localPosition.y, theDoor.transform.localPosition.z);
			theDoor.transform.localPosition = newPosition;
			yield return new WaitForSeconds (.05f);
		}
		Destroy (this);
	}

}
