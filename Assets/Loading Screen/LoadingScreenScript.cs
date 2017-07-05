using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreenScript : MonoBehaviour {

	const float ASYNC_LOAD_COMPLETION_PROGRESS = 0.9f;

	public string LoadingSceneName;
	public float MinimumLoadTime;
	public float FadeTime;
	public Color FadeColor;
	public delegate void UpdateLoadingProgress(float progress);
	public UpdateLoadingProgress UpdateProgress; //Can be used to update progress of loading, 0.0 to 1.0

	Image fadeImage;
	CanvasGroup canvasGroup;
	AsyncOperation loadingOperation;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject); //Don't destory us on load
		//Get component references
		fadeImage = GetComponent<Image>();
		canvasGroup = GetComponent<CanvasGroup>();
	}
	
	void Start () {
		//We don't block raycasts, and we can't be interacted with
		canvasGroup.blocksRaycasts = false;
		canvasGroup.interactable = false;

		LoadLevel("AfterScene");
	}

	//Public call
	public void LoadLevel(string sceneName) {
		StartCoroutine(LoadSceneAsync(sceneName));
	}

	IEnumerator LoadSceneAsync(string sceneName) {
		fadeImage.color = FadeColor; //Set the color of the fade

		yield return StartCoroutine(FadeInAsync()); //Fade in
		yield return SceneManager.LoadSceneAsync(LoadingSceneName); //Load loading screen
		yield return StartCoroutine(FadeOutAsync()); //Fade out, revealing loading screen

		float endTime = Time.time + MinimumLoadTime; //When we should fade out to our scene
		loadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive); //Additively load the scene
		loadingOperation.allowSceneActivation = false; //Don't let the scene activate yet.

		while (!loadingOperation.isDone) { //While we're not done loading and haven't been activated yet,
			if(loadingOperation.progress < ASYNC_LOAD_COMPLETION_PROGRESS) { //If Unity hasn't loaded the level fully yet,
				if(UpdateProgress != null) { //Delegate is not null,
					UpdateProgress(loadingOperation.progress / ASYNC_LOAD_COMPLETION_PROGRESS);
				}
			} else { //Otherwise, we're done loading,
				if (UpdateProgress != null) { //Delgeate is not null,
					UpdateProgress(1.0f); //We're done loading
				}
				
				if (Time.time < endTime) { //If loading was too quick,
					yield return new WaitForSeconds(endTime - Time.time); //Wait remaining time
				}
				yield return StartCoroutine(FadeInAsync()); //Fade in
				loadingOperation.allowSceneActivation = true; //Allow loading operation to complete,
			}
			yield return null; //Wait
		}
		yield return SceneManager.UnloadSceneAsync(LoadingSceneName); //Unload loading screen
		yield return StartCoroutine(FadeOutAsync()); //Fade out, revealing loaded scene

		yield break; //We're done!
	}

	IEnumerator FadeOutAsync() {
		float timer = 0;

		canvasGroup.alpha = 0;

		while (timer <= FadeTime) {
			timer += Time.deltaTime;
			canvasGroup.alpha = (FadeTime - timer) / FadeTime;
			yield return null;
		}
	}

	IEnumerator FadeInAsync() {
		float timer = 0;
		canvasGroup.alpha = 1;

		while (timer <= FadeTime) {
			timer += Time.deltaTime;
			canvasGroup.alpha = 1.0f - ((FadeTime - timer) / FadeTime);
			yield return null;
		}
	}
}
