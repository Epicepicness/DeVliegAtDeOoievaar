using UnityEngine;
using System.Collections;

public class AreaManager : MonoBehaviour {

	//De AreaManager is een component op een Area object. Het heeft alle informatie voor één specifieke area, en spawned deze area ook wanneer de AreaManager gemaakt word.
	//De Area objecten worden geinstantieerd in de GameManager.

	GameManager gameManager;								//De GameManager.

	[Header("Inspector Data")] //---------------------------------------------------------------------------------------------------------------------------------------------------
	[Tooltip("areaName is de naam van de Area (wordt niet gebruikt in code).")] 
	public string areaName;
	[Tooltip("areaNumber is het nummer van de Area (wordt niet gebruikt in code).")] 
	public int areaNumber;
	[Tooltip("endingArea is een boolean die bepaald of een area een laatste in het spel is of niet.")] 
	public bool endingArea = false;

	[Header("Sound and Music Settings")] //---------------------------------------------------------------------------------------------------------------------------------------------------
	[Tooltip("backGroundSound is de AudioClip die in de achtergrond speelt in deze area.")] 
	public AudioClip backGroundSound;
	[Tooltip("footstepSound is de AudioClip die als voet stap geluid speelt in deze area.")] 
	public AudioClip footstepSound;

	[Header("Keuze Punt Settings")] //---------------------------------------------------------------------------------------------------------------------------------------------------
	[Tooltip("keuzeRechts is het nummer van de volgende Area als de speler naar rechts zou gaan.")] 
	public int keuzeRechts;
	[Tooltip("keuzeLinks is het nummer van de volgende Area als de speler naar links zou gaan.")] 
	public int keuzeLinks; 

	[Space(10)] [Tooltip("maxRotationRight is de hoeveelheid die de speler naar rechts roteerd om een keuze te maken en de volgende area te spawnen.")]
	public float maxRotationRight = 270;
	[Tooltip("maxRotationLEFT is de hoeveelheid die de speler naar links roteerd om een keuze te maken en de volgende area te spawnen.")]
	public float maxRotationLeft = 90;
	[Tooltip("rotationSpeedRight is de basis snelheid waarmee de speler naar rechts roteerd bij het maken van een keuze.")]
	public float rotationSpeedRight = 45;
	[Tooltip("rotationSpeedLeft is de basis snelheid waarmee de speler naar links roteerd bij het maken van een keuze.")]
	public float rotationSpeedLeft = 45;
	[Tooltip("smoothRotate versnelt de rotatie iets in het midden, maar remt het af namate je dichter bij een keuze richting komt (WERKT NOG NIET).")]
	public bool smoothRotate = true;

	[Space(10)] [Tooltip("keuzePuntPrefab is het KeuzePunt Prefab die aan het einde van het pad.")] 
	public GameObject keuzePuntPrefab;

	[Tooltip("wachtKeuze is een boolean die bepaald of een keuze gemaakt wordt door te wachten of niet.")] 
	public bool wachtKeuze = false;
	[Tooltip("wachtTijd is een float die aangeeft hoe lang de speler moet wachten voor de keuze voorbij gaat (alleen in het geval van een wachtKeuze).")] 
	public float wachtTijd = 5f;
	GameObject keuzeObject;
	[HideInInspector] public bool keuzeBezig;
	private float keuzeTimer;							//Interne timer voor het bijhouden van de keuzeTijd.

	[Header("Prefab Path Settings")] //---------------------------------------------------------------------------------------------------------------------------------------------------
	[Tooltip("numberToSpawn is de hoeveelheid Prefabs (uit de areaPieces array) die gespawned worden voor het pad.")] 
	public int numberToSpawn;
	[Tooltip("inOrder bepaald of de Prefabs voor het pad in volgorde van de array worden gespawned of in willekeurige volgorde (true betekend array volgorde).")] 
	public bool spawnInOrder = false;

	[Tooltip("areaPieces[] is de array met alle Prefab onderdelen die het pad gaan vormen.")] 
	public GameObject[] areaPieces;

	GameObject keuzePunt;

	//Ieder area piece prefab moet een child hebben genaamd "Ground", die als x.scale de totale lengte van het prefab heeft.

	//Awake zoekt de gameManager op.
	void Awake () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void Update () {
		if (keuzeBezig && wachtKeuze) {
			keuzeTimer += Time.deltaTime;
			if (keuzeTimer > wachtTijd) {
				DoorLoopKeuze (false);
			}
		}		
	}

	//SpawnArea spawned alle areaPieces onderdelen, en vervolgens een Keuzepunt prefab. Wordt aangeroepen vanuit de GameManager.
	public float SpawnArea (float spawnStart, GameObject vorigKeuzePunt) {
		float totalLength = spawnStart;						//totalLength is het punt op de x-as waar het volgende object op gespawned moet worden.

		if (vorigKeuzePunt) {								//Voegt het vorige keuze punt toe als Child van dit Area object, zodat deze ook destroyed word wanneer deze area voorbij is.
			vorigKeuzePunt.transform.parent = this.transform;
			totalLength += vorigKeuzePunt.transform.Find ("Ground").localScale.x / 2;
		}

		//Spawned de onderdelen van het pad op de juiste afstanden.
		for (int i = 0; i < numberToSpawn; i++) {
			int randomPiece = (spawnInOrder && i > areaPieces.Length - 1) ? i - areaPieces.Length * (i / areaPieces.Length) : ((spawnInOrder) ? i : Random.Range (0, areaPieces.Length));

			float lengthObject = areaPieces [randomPiece].transform.Find ("Ground").localScale.x;
			Vector3 spawnPosition = new Vector3(totalLength + (lengthObject/2), 0, 0);

			GameObject part = (GameObject)Instantiate (areaPieces[randomPiece], spawnPosition, Quaternion.identity);
			part.transform.parent = this.transform;

			totalLength += lengthObject;
		}

		//Spawned het keuzepunt op de juiste afstand.
		if (!endingArea) {
			float lengthKeuzePunt = keuzePuntPrefab.transform.Find ("Ground").localScale.x / 2;
			keuzePunt = (GameObject)Instantiate (keuzePuntPrefab, new Vector3 (totalLength + lengthKeuzePunt, 0, 0), Quaternion.identity);
			gameManager.SaveKeuzePunt (keuzePunt);

			return totalLength + lengthKeuzePunt;
		}

		return totalLength;
	}

	public void DoorLoopKeuze (bool right) {
		keuzeObject = GameObject.Find ("Metro-Cabine");
		if (right) {
			keuzeBezig = false;
			keuzeTimer = 0;
			StartCoroutine (RotateWachtKeuze ());
		} else if (keuzeBezig) {
			Destroy (keuzeObject, 2);
			//keuzeObject.GetComponent <DoorOpener> ().StartOpenDoor ();
			gameManager.StageForward (false);
		}
	}

	//RotatePlatform roteerd het keuzePunt object om de illusie te wekken dat de speler roteerd; als het platform genoeg geroteerd is geeft die het OK dat de keuze gemaakt is.
	//Wordt aangeroepen vanuit de Player script.
	public bool RotatePlatform (bool right) {
		if (right) {
			float rotationSpeed = (smoothRotate) ? rotationSpeedRight : rotationSpeedRight;
			keuzePunt.transform.Rotate (-Vector3.up * rotationSpeed * Time.deltaTime);

			float tempAngle = Mathf.Round (keuzePunt.transform.rotation.eulerAngles.y);
			if (tempAngle <= maxRotationRight + 1 && tempAngle >= maxRotationRight - 1) {
				return true;
			}

		} else {
			float rotationSpeed = (smoothRotate) ? rotationSpeedRight : rotationSpeedLeft;
			keuzePunt.transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);


			float tempAngle = Mathf.Round (keuzePunt.transform.rotation.eulerAngles.y);
			if (tempAngle >= maxRotationLeft - 1 && tempAngle <= maxRotationLeft + 1) {
				return true;
			}
		}

		return false;
	}

	IEnumerator RotateWachtKeuze () {

		Debug.Log (keuzePunt.transform.rotation.eulerAngles.y);

		while(keuzePunt.transform.rotation.eulerAngles.y >= 91 || keuzePunt.transform.rotation.eulerAngles.y <= 89) {
			Debug.Log (keuzePunt.transform.rotation.eulerAngles.y);

			Debug.Log (keuzePunt.transform.rotation.eulerAngles.y <= 91);
			Debug.Log (keuzePunt.transform.rotation.eulerAngles.y >= 89);


			keuzePunt.transform.Rotate (Vector3.up * 180 * Time.deltaTime);

			yield return new WaitForSeconds (.10f);
		}

		gameManager.StageForward (true);

	}

}
