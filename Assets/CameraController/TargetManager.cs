using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour {

	public CameraController cc;
	public GameObject TargetPrefab;
	public float spawnRate;

	float spawnTimer;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.deltaTime;
		if(spawnTimer >= spawnRate){
			spawnTimer = 0;
			GameObject spawn = (GameObject) Instantiate(TargetPrefab, transform.position, Quaternion.identity);
			cc.AddTarget(spawn);
		}
	}
}
