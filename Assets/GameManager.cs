using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Managers/GameManager")]

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public static GameManager Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (GameManager)FindObjectOfType(typeof(GameManager)); //Find it
			}
			return instance; //Return it
		}
	}

	public enum State { Null };
	public State state;

	void Awake(){
		DontDestroyOnLoad(transform.gameObject); //Don't destroy us on loading new scenes
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel(string level){
		SceneManager.LoadScene(level);
	}

	public State GetState(){
		return state;
	}

	public void SetState(State newState){
		state = newState;
	}
}
