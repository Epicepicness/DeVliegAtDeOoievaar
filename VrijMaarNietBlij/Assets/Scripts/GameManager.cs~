using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	//De GameManager class dient als een centraal punt waar in de andere onderdelen geinstantieerd en aangeroepen worden. Ook worden hier centrale variabelen opgeslagen.

	[HideInInspector] public float spawnNextDistance;		//Houd bij hoe ver de areas gespawned moeten worden op de x-as.
	[HideInInspector] public int currentStage;				//Houd het getal bij van de area waar in de speler zich op het moment bevind.
	[HideInInspector] public GameObject currentAreaManager;	//Het Area object dat momenteel gebruikt wordt. 

	[Tooltip("areaArray[] is de array van alle 'Area objecten', die verantwoordelijk zijn voor het instantieëren van de areas. - Voor area nummers zie LogischeMap")] 
	public GameObject[] areaArray;

	GameObject laatsteKeuzePunt;							//Het vorige keuzepunt object (zodat deze verweiderd kan worden wanneer die niet meer nodig is).
	GameObject player;										//Het Speler object.
	GameObject soundManager;								//Het SoundManager object.

	//Awake zet de speler en SoundManager variabele, en instantieert een speler en SoundManager object als deze nog niet bestaan.
	void Awake () {
		player = (GameObject.Find ("Player")) ? GameObject.Find ("Player") : 
			(GameObject)Instantiate (Resources.Load("Player"), Vector3.zero, Quaternion.identity);
		soundManager = (GameObject.Find ("SoundManager")) ? GameObject.Find ("SoundManager") : 
			(GameObject)Instantiate (Resources.Load("SoundManager"), Vector3.zero, Quaternion.identity);

		AudioListener.volume = 1;
	}

	//Start zet de initiële spawnDistance voor objecten (gelijk aans de speler's positie), en start het spawnen van de eerste area. 
	void Start () {
		spawnNextDistance = (player) ? player.transform.position.x : 0;

		SpawnNextSegment ();
	}

	//nextSegment verweiderd de volgende area, en instantieërd een Area object om de volgnede area te spawnen.
	void SpawnNextSegment () {
		if (currentAreaManager != null) {
			Destroy (currentAreaManager);
		}
		
		currentAreaManager = Instantiate(areaArray[currentStage]);

		if (currentAreaManager.GetComponent<AreaManager> ().backGroundSound)
			soundManager.GetComponent<SoundManager> ().SwapBackgroundSound (currentAreaManager.GetComponent<AreaManager> ().backGroundSound);
		if (currentAreaManager.GetComponent<AreaManager> ().footstepSound)
			player.GetComponent<Player> ().setFootstepSound (currentAreaManager.GetComponent<AreaManager> ().footstepSound);
		
		spawnNextDistance = currentAreaManager.GetComponent<AreaManager> ().SpawnArea (spawnNextDistance, laatsteKeuzePunt);
	}

	//stageForward zet de currentStage gebaseerd op doorgegeven keuze, en roept de NextSegment() aan.
	public void StageForward (bool right) {
		currentStage = (right) ? currentAreaManager.GetComponent<AreaManager>().keuzeRechts : currentAreaManager.GetComponent<AreaManager>().keuzeLinks;
		player.GetComponent<Player> ().movable = true;

		SpawnNextSegment ();
	}

	//Slaat het laatst gebruikte keuzePunt op (aangeroepen vanuit AreaManager), zodat deze later verweirderd kan worden wanneer die niet meer nodig is.
	public void SaveKeuzePunt (GameObject keuzePunt) {
		laatsteKeuzePunt = keuzePunt;
	}

	//OnDrawGizmos tekent een rode bal in Unity die het volgende keuze punt aangeeft.
	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (new Vector3(spawnNextDistance, 0, 0), 1);

		if (GameObject.Find ("Player")) {
			player = GameObject.Find ("Player");

			Gizmos.color = Color.blue;
			Gizmos.DrawSphere (player.transform.position, 1);
		} 
	}

} 
