using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystemExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Inventory inventory = new Inventory(100);

		inventory.SortByName();
		Debug.Log("Name\n" + inventory);

		inventory.SortByRarity();
		Debug.Log("Rarity\n" + inventory);

		inventory.SortByValue();
		Debug.Log("Value\n" + inventory);

		inventory.SortByObtained();
		Debug.Log("Obtained\n" + inventory);

		inventory.SortByWeight();
		Debug.Log("Weight\n" + inventory);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
