using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]

public class HeadBobber : MonoBehaviour {

	//Deze class regeld de head bobbing van de speler wanneer deze loopt.
	//Deze class werkt met de power of magic, dat is alles dat je hoeft te weten.

	[Tooltip("bobbingDelayTime is de vertraging tussen het beginnen met lopen en het starten van de headbobs (hoger getal betekend langere vertraging)")]
	public float bobbingDelayTime = .08f;												//Dit is ter voorkoming dat de camera jitterd wanneer de loop toets gespammed word.
	[Tooltip("bobbingSpeed bepaald de tijd tussen 'bobs' (lager getal betekend meer 'bobs').")]
	public float bobbingSpeed = 0.16f;
	[Tooltip("bobbingAmount is hoe heftig de 'bobs' zijn (hoger getal betekend grotere camera beweging per 'bob').")]
	public float bobbingAmount = 0.2f;

	float midPoint = 1.0f;
	float timer = 0f;
	float delay = 0f;

	Camera playerCamera;
	Player player;

	void Awake () {
		playerCamera = GameObject.Find ("Player Camera").GetComponent<Camera> ();
		if (!playerCamera) {
			Debug.LogError ("No 'Player Camera' was found as child of Player Object");
		}

		player = GetComponent <Player> ();
	}

	void Start () {
		midPoint = playerCamera.transform.localPosition.y;
	}

	void LateUpdate () {
		float waveslice = 0.0f;

		bool moving = player.moving;

		if (!moving) {
			timer = 0.0f;
			delay = 0.0f;
		} else if (delay < bobbingDelayTime) { 
			delay += Time.deltaTime;
		} else {
			waveslice = Mathf.Sin(timer);
			timer += bobbingSpeed;
			if (timer > Mathf.PI * 2) {
				timer = timer - (Mathf.PI * 2);
			}
		}

		if(waveslice != 0) {
			float translateChange = waveslice * bobbingAmount;
			float totalAxes = 1;
			totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
			translateChange = totalAxes * translateChange;

			Vector3 tempPosition = playerCamera.transform.localPosition;
			tempPosition.y = midPoint + translateChange;
			playerCamera.transform.localPosition = tempPosition;
		} else {
			Vector3 tempPosition = playerCamera.transform.localPosition;
			tempPosition.y = midPoint;
			playerCamera.transform.localPosition = tempPosition;
		}
	}

}
