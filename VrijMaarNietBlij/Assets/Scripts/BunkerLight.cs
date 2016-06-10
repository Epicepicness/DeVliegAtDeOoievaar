using UnityEngine;
using System.Collections;

public class BunkerLight : MonoBehaviour {

	public float pushForce = 2f;

	Rigidbody rB;
	bool right;

	//Alle shit waarvan je denkt dat het werkt werkt voor geen kut; maar dit is een tijdelijke oplossing die soort van werkt.

	void Start () {
		rB = GetComponent<Rigidbody> ();

		rB.AddForce (Random.Range (-2, 2), 0, pushForce);
	}

	void Update () {

		if (right && Mathf.Sign(rB.velocity.z) == -1) {
			Debug.Log (rB.velocity.z);
			rB.velocity = new Vector3 (-rB.velocity.x, rB.velocity.y, rB.velocity.z - 0.15f);

			right = false;
		}else if (!right && Mathf.Sign(rB.velocity.z) == 1) {
			rB.velocity = new Vector3 (-rB.velocity.x, rB.velocity.y, rB.velocity.z + 0.15f);
			right = true;
		}
		//rB.AddForce(0, Random.Range(-pushForce, pushForce), Random.Range(-pushForce, pushForce) * Random.Range(10F, 20F));
	}
}
