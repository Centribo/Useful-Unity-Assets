using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public Vector3 direction;
	public float speed;

	// Use this for initialization
	void Start () {
		direction = new Vector3(Random.value, Random.value, 0);
		speed = Random.value * 2;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += direction.normalized * speed * Time.deltaTime;
	}
}
