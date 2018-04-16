using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader {
	
	// https://docs.unity3d.com/ScriptReference/AsyncOperation-progress.html
	const float ASYNC_LOAD_COMPLETION_PROGRESS = 0.9f;

	[Range(-1, 1)]
	public float progress = -1; //-1 = Level load not initiated, [0, 1] = loading progress
	public delegate void UpdateLoadingProgress(float progress);
	public UpdateLoadingProgress UpdateProgress; //Can be used to update progress of loading, 0.0 to 1.0

	private bool deleteOnLoad = false;
	private bool autoLoad = true;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject); //Auto set to not destroy
	}

	public void LoadScene(string sceneName, bool autoLoad = true, bool deleteOnLoad = false, LoadSceneMode loadMode = LoadSceneMode.Additive){
		this.autoLoad = autoLoad;
		this.deleteOnLoad = deleteOnLoad;
		
		StartCoroutine(LoadSceneAsync(sceneName, loadMode));
	}

	public void ActivateScene(){
		this.autoLoad = true;
	}

	IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadMode){
		AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName, loadMode); //Asynchronously load the scene
		loadingOperation.allowSceneActivation = false; //Don't let the scene activate yet.

		while(!loadingOperation.isDone){ //While we're not done loading,
			if(loadingOperation.progress >= ASYNC_LOAD_COMPLETION_PROGRESS){ //If the process of loading the scene isn't complete yet,
				if(UpdateProgress != null){ //Call back for progress isn't null,
					UpdateProgress(loadingOperation.progress/ASYNC_LOAD_COMPLETION_PROGRESS); //Call it [0, 1]
				}
				if(this.autoLoad){ //If we're set to start scene activation, do so
					loadingOperation.allowSceneActivation = true;
				}
			}

			yield return null; //Update loop
		}

		//Delete on load if nesscessary
		if(this.deleteOnLoad){
			Destroy(gameObject);
		} else {
			progress = -1;
		}

		yield return null;
	}
}
