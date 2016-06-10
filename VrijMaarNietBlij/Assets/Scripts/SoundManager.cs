using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	//"Dance on my balls, cats fcking handbags, yours only yours, oh look a seagull dance band, it's no lie, lisa and the clown said look Harry had vagina malfunction.";

	//De SoundManager class regelt alle geluids aspecten binnen het spel, waaronder achtergrond geluid en volumes.

	[Header("Background Sound Settings")]
	[Tooltip("backgroundSound1 is één van de twee backgroundSound AudioSources. Deze wordt geset vanuit de GameManager met de clip uit de AreaManager. " +
		"Er zijn twee background AudioSources in verband met fade in/out overgang")]
	public AudioSource backgroundSound1;
	[Tooltip("backgroundSound2 is één van de twee backgroundSound AudioSources. Deze wordt geset vanuit de GameManager met de clip uit de AreaManager. " +
		"Er zijn twee background AudioSources in verband met fade in/out overgang")]
	public AudioSource backgroundSound2;

	[Tooltip("backgroundSoundVolume is het volume van de achtergrond geluiden (hoger getal betekend harder geluid).")]
	public float backgroundSoundVolume = 0.4f;
	[Tooltip("soundFadeSpeed is de snelheid waarmee geluid fade (hoger getal betekend kortere fade in/out tijd).")]
	public float soundFadeSpeed = 0.05f;

	//SwapBackgroundSound neemt een AudioClip en laat deze afspelen door de ene AudioSource, terwijl de andere AudioSource wordt gestopt.
	public void SwapBackgroundSound (AudioClip clip) {

		if (backgroundSound1.isPlaying && backgroundSound1.clip != clip) {			//Als AudioSource1 speelt en dezelfde clip niet al wordt gespeelt start AudioSource2
			backgroundSound2.clip = clip;

			StartCoroutine (StartMusic (backgroundSound2));
			StartCoroutine (FadeMusic (backgroundSound1));
		} else if (backgroundSound2.isPlaying && backgroundSound2.clip != clip) { 	//Als AudioSource2 speelt en dezelfde clip niet al wordt gespeelt start AudioSource1
			backgroundSound1.clip = clip;

			StartCoroutine (StartMusic (backgroundSound1));
			StartCoroutine (FadeMusic (backgroundSound2));
		} else {																	//Als geen van twee spelen start hij met AudioSource 1.
			backgroundSound1.clip = clip;

			StartCoroutine (StartMusic (backgroundSound1));
			StartCoroutine (FadeMusic (backgroundSound2));
		}
	}

	public void MuteMusic () {
		if (backgroundSound1.isPlaying)
			FadeMusic (backgroundSound1);
		if (backgroundSound2.isPlaying)
			FadeMusic (backgroundSound2);
	}

	//StartMusic() neemt een AudioSource en laat die afspelen, terwijl het volume langzaam wordt verhoogt tot aan het gewenste volume.
	IEnumerator StartMusic(AudioSource a) {
		a.Play ();
		while(a.volume < backgroundSoundVolume) {
			a.mute = false;
			a.volume = a.volume + soundFadeSpeed;
			yield return new WaitForSeconds (.15f);
		}
		a.volume = backgroundSoundVolume;
	}

	//FadeMusic neemt een AudioSource en verlaagt langzaam het volume tot 0, en stopt vervolgens het afspelen van de AudioSource.
	IEnumerator FadeMusic(AudioSource a) {
		while(a.volume >= soundFadeSpeed) {
			a.volume = a.volume -soundFadeSpeed;
			yield return new WaitForSeconds (.15f);
		}
		a.volume = 0;
		a.Stop();
	}

}
