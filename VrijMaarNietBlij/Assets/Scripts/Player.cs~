using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//Het Player script regeld de speler input en het vooruit bewegen van de speler.

	[HideInInspector] public bool moving;					//Boolean die doorgeeft of de speler in beweging is (ivm headbob).
	[HideInInspector] public bool movable = true;			//Of de speler vooruit mag bewegen (ivm keuzes / wachten).
	[HideInInspector] public bool forcedToMove = false;		//Boolean die de speler dwingt om te lopen.
	[HideInInspector] AudioSource footstepSound;			//footstepSound is de AudioSource voor de voetstappen.

	[Header("Player Movement")]
	[Tooltip("moveSpeed is de maximum voorwaartse snelheid van de speler bij het lopen.")]
	public float moveSpeed;
	[Tooltip("accelerationTime is de tijd die het de speler kost om de maximum snelheid te bereiken en af te remmen (remmen is half).")]
	public float accelerationTime = .2f;

	[Space(10)] [Tooltip("gameManager is het GameManager object, wordt automatisch ingevuld als leeg.")] 
	public GameManager gameManager;

	float velocityXSmooth;									//De currentVelocity in de smoothdamp.
	Vector3 velocity;										//Slaat de current snelheid op.

	//Awake zoekt de gameManager op indien die nog niet is ingevuld in de inspector.
	void Awake () {
		if (!gameManager) {
			gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		}

		footstepSound = GetComponent<AudioSource> ();
	}

	//De Update functie kijkt voor input en voert bijbehorende code uit.
	void Update () {
		moving = false;

		//Als de voorwaarts toets gebruikt wordt en de speler mag bewegen beweegt de speler vooruit.
		//if (Input.GetKey (KeyCode.W) && movable) {
		if (((Input.GetAxis ("Vertical") < 0 || Input.GetAxis ("MainJoystickVertical") < 0) && movable) || forcedToMove) {
			velocity.x = Mathf.SmoothDamp (velocity.x, moveSpeed, ref velocityXSmooth, accelerationTime);
			Move (velocity);
		} else if (velocity.x > .1f && movable) { 
			velocity.x = Mathf.SmoothDamp (velocity.x, 0, ref velocityXSmooth, accelerationTime/2);
			Move (velocity);
		} else {
			velocity.x = 0;
			footstepSound.Stop ();
		}

		//Wanneer de speler op een keuzePunt aangekomen is mag de speler tijdelijk niet bewegen tot er een keuze gemaakt is.
		if (gameManager.currentAreaManager.GetComponent<AreaManager> ().wachtKeuze) {
			if (transform.position.x >= gameManager.spawnNextDistance && movable) {
				movable = false;
				gameManager.currentAreaManager.GetComponent<AreaManager> ().DoorLoopKeuze (true);
			} 
			else if (transform.position.x >= gameManager.spawnNextDistance - 20 && movable) {
				gameManager.currentAreaManager.GetComponent<AreaManager> ().keuzeBezig = true;
			}
		}

		if (transform.position.x >= gameManager.spawnNextDistance && movable) {
			movable = false;
		} 

		//Wanneer de speler bezig is met een keuze moment en op D drukt kiest de speler voor rechts.
		//if (Input.GetKey(KeyCode.D) && !movable) {
		if ((Input.GetAxis ("Horizontal") < 0 || Input.GetAxis ("MainJoystickHorizontal") < 0) && !movable) {

			if (gameManager.currentAreaManager.GetComponent<AreaManager>().RotatePlatform(true)) {
				gameManager.StageForward (true);
			}
		} 

		//Wanneer de speler bezig is met een keuze moment en op A drukt kiest de speler voor links.
		//if (Input.GetKey(KeyCode.A) && !movable) {
		if ((Input.GetAxis ("Horizontal") > 0 || Input.GetAxis ("MainJoystickHorizontal") > 0) && !movable) {

			if (gameManager.currentAreaManager.GetComponent<AreaManager>().RotatePlatform(false)) {
				gameManager.StageForward (false);
			}
		} 
	}

	//Move beweegt de speler door een transform.Translate.
	void Move (Vector3 velocity) {
		transform.Translate (velocity);
		moving = true;

		Footsteps ();
	}

	void Footsteps () {
		if (footstepSound.isPlaying == false) {

			footstepSound.volume = Random.Range (0.8f, 1);			
			footstepSound.pitch = Random.Range (0.8f, 1.2f);

			footstepSound.Play ();
		}
	}

	public void setFootstepSound(AudioClip a) {
		footstepSound.clip = a;
	}

}
