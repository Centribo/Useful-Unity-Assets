using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystemExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Item itemA = new Item("A", 100, 15, (int)Item.Rarities.Normal, true, 1);
		Item itemB = new Item("B", 100, 15, (int)Item.Rarities.Normal, true, 1);
		Item itemC = new Item("C", 1000, 50, (int)Item.Rarities.Epic, false, 1);

		Inventory inventoryA = new Inventory(100);
		Inventory inventoryB = new Inventory(100);

		inventoryA.AddItem(itemA);
		inventoryA.AddItem(itemA);
		inventoryA.AddItem(itemA);
		inventoryA.AddItem(itemA);

		inventoryA.SortByName();
		Debug.Log("Name\n" + inventoryA);
		//inventoryB.SortByName();
		//Debug.Log("Name\n" + inventoryB);

		inventoryA.RemoveItems(itemA, 2);

		inventoryA.SortByName();
		Debug.Log("Name\n" + inventoryA);
		//inventoryB.SortByName();
		//Debug.Log("Name\n" + inventoryB);

		//inventory.SortByRarity();
		//Debug.Log("Rarity\n" + inventory);

		//inventory.SortByValue();
		//Debug.Log("Value\n" + inventory);

		//inventory.SortByObtained();
		//Debug.Log("Obtained\n" + inventory);

		//inventory.SortByWeight();
		//Debug.Log("Weight\n" + inventory);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
