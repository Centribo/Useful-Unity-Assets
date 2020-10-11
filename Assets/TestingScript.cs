using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

	// Start is called before the first frame update
	void Start() {
		List<Tuple<int, char>> samples = new List<Tuple<int, char>> {
			Tuple.Create(3, 'A'),
			Tuple.Create(2, 'B'),
			Tuple.Create(9, 'C')
		};
		char sample = samples.GetWeightedRandomElement();
	}

	// Update is called once per frame
	void Update() {

	}
}
