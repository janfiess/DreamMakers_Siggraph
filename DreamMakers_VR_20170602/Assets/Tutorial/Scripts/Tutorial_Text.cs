using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Text : MonoBehaviour {

	public TextMesh statusText;
	public TextMesh scenarioStopwatchText;
	public TextMesh hudMessage;


	// FadeText in and out START
	public void ShowMesageForSeconds_trigger(string text, float displayTime){
		StartCoroutine(ShowTextForSeconds(text, statusText, displayTime));
	}

	public void FadeOutText_trigger(TextMesh targetTextMesh){
		StartCoroutine(FadeOutText(targetTextMesh));
	}

	public void WaitThenFadeOutText_trigger(TextMesh targetTextMesh){
		StartCoroutine(WaitThenFadeOutText(targetTextMesh));
	}

	IEnumerator ShowTextForSeconds(string text, TextMesh targetTextMesh,float displayTime){
		statusText.text = text;

		float duration = 1.0f;
		// Make transparent, but maintain r, g, b
		Color prevColor = new Color(targetTextMesh.color.r, targetTextMesh.color.g, targetTextMesh.color.b, 0);

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration) {
			float alpha = Mathf.Lerp (prevColor.a, 1.0f, t);
			Color newColor =  new Color(prevColor.r, prevColor.g, prevColor.b, alpha);
			targetTextMesh.color = newColor;
			yield return null;
		}

		Color fullOpacity = new Color(targetTextMesh.color.r, targetTextMesh.color.g, targetTextMesh.color.b, 1);
		targetTextMesh.color = fullOpacity;

		yield return new WaitForSeconds (displayTime);
		StartCoroutine(FadeOutText(targetTextMesh));
	}

	IEnumerator FadeOutText(TextMesh targetTextMesh){
		float duration = 0.8f;
		Color prevColor = targetTextMesh.color;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration) {
			float alpha = Mathf.Lerp (prevColor.a, 0.0f, t);
		
			Color newColor =  new Color(prevColor.r, prevColor.g, prevColor.b, alpha);;
			targetTextMesh.color = newColor;
			yield return null;
		}
		Color fullTransparency = new Color(targetTextMesh.color.r, targetTextMesh.color.g, targetTextMesh.color.b, 0);
		targetTextMesh.color = fullTransparency;
		targetTextMesh.text = "";
	}

	IEnumerator WaitThenFadeOutText(TextMesh targetTextMesh){
		yield return new WaitForSeconds (2.0f);
		float duration = 0.8f;
		Color prevColor = targetTextMesh.color;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration) {
			float alpha = Mathf.Lerp (prevColor.a, 0.0f, t);

			Color newColor =  new Color(prevColor.r, prevColor.g, prevColor.b, alpha);;
			targetTextMesh.color = newColor;
			yield return null;
		}
		Color fullTransparency = new Color(targetTextMesh.color.r, targetTextMesh.color.g, targetTextMesh.color.b, 0);
		targetTextMesh.color = fullTransparency;
		targetTextMesh.text = "";
	}




	// FadeText in and out END

}
