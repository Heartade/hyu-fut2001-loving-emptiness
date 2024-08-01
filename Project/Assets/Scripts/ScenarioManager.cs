using System.Collections;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
	public GameObject title;
	public GameObject titleButton;
	public GameObject intro1;
	public GameObject intro2;
	public GameObject howto;
	public GameObject howtoButton;
	public GameObject brushL;
	public GameObject brushR;
	public GameObject skyboxPortal;
	public ColorChange brushScriptL;
	public ColorChange brushScriptR;
	public AudioSource audioSource;
	float revLevel = 0;
	float maxLowPass = 4000;
	float minLowPass = 22000;
	float maxDecayTime = 4.5f;
	float minDecayTime = 0;
	float maxRevLevel = 0;
	float minRevLevel = -10000;
	float maxVolume = 0.3f;
	float minVolume = 1f;
	bool isStarted = false;
	public void OnProceedStart()
	{
		StartCoroutine(FadeOut(title.GetComponent<SpriteRenderer>(), 0));
		StartCoroutine(FadeOut(titleButton.GetComponent<SpriteRenderer>(), 0));
		StartCoroutine(FadeIn(intro1.GetComponent<SpriteRenderer>(), 1));
		StartCoroutine(FadeOut(intro1.GetComponent<SpriteRenderer>(), 5));
		StartCoroutine(FadeIn(intro2.GetComponent<SpriteRenderer>(), 6));
		StartCoroutine(FadeOut(intro2.GetComponent<SpriteRenderer>(), 9));
		StartCoroutine(FadeIn(howto.GetComponent<SpriteRenderer>(), 10));
		StartCoroutine(FadeIn(howtoButton.GetComponent<SpriteRenderer>(), 10));
	}
	public void OnProceedIntro()
	{
		StartCoroutine(FadeOut(howto.GetComponent<SpriteRenderer>(), 0));
		StartCoroutine(FadeOut(howtoButton.GetComponent<SpriteRenderer>(), 0));
		brushL.SetActive(true);
		brushR.SetActive(true);
		isStarted = true;
		StartCoroutine(ActivateAfter(skyboxPortal, 1));
	}
	IEnumerator ActivateAfter(GameObject target, float delay)
	{
		yield return new WaitForSeconds(delay);
		target.SetActive(true);
	}
	IEnumerator FadeIn(SpriteRenderer renderer, float delay)
	{
		Color newColor = renderer.color;
		newColor.a = 0;
		renderer.color = newColor;
		yield return new WaitForSeconds(delay);
		renderer.gameObject.SetActive(true);
		while ( renderer.color.a != 1 )
		{
			newColor.a = Mathf.MoveTowards(newColor.a, 1, Time.deltaTime);
			renderer.color = newColor;
			yield return new WaitForEndOfFrame();
		}
	}
	IEnumerator FadeOut(SpriteRenderer renderer, float delay)
	{
		yield return new WaitForSeconds(delay);
		Color newColor = renderer.color;
		newColor.a = 1;
		while ( renderer.color.a != 0 )
		{
			newColor.a = Mathf.MoveTowards(newColor.a, 0, Time.deltaTime);
			renderer.color = newColor;
			yield return new WaitForEndOfFrame();
		}
		renderer.gameObject.SetActive(false);
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if ( isStarted )
		{
			float drawDist = brushScriptL.drawDist + brushScriptR.drawDist;
			float targetRev = Mathf.Min(1, drawDist / 200);
			revLevel = Mathf.MoveTowards(revLevel, targetRev, Time.deltaTime / 2);
		}
		audioSource.volume = Mathf.Lerp(maxVolume, minVolume, revLevel);
		audioSource.GetComponent<AudioReverbFilter>().reverbLevel = Mathf.Lerp(maxRevLevel, minRevLevel, revLevel);
		audioSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = Mathf.Lerp(maxLowPass, minLowPass, revLevel);
		audioSource.GetComponent<AudioReverbFilter>().decayTime = Mathf.Lerp(maxDecayTime, minDecayTime, revLevel);
	}
}
