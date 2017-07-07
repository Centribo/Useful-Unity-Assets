using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

	int maxWeight;
	int currentWeight;
	int lastAdded;
	List<Item> items;

	public Inventory(int maxWeight) {
		this.maxWeight = maxWeight;
		this.items = new List<Item>();
		this.currentWeight = 0;
		this.lastAdded = -1;
	}

	public bool AddItem(Item i) { // True if added to inventory, false if not
		if (i.isStackable) { // If item can be stacked,
							 // Check if we can add the stack by weight
			if (currentWeight + (i.weight * i.stackSize) > maxWeight) {
				return false;
			} else {
				Item j = FindItem(i); // Look for the item,
				if(j == null) { // If we don't already have it in our inventory,
					items.Add(i);
					lastAdded++;
					i.updatedOrder = lastAdded;
					currentWeight += i.weight * i.stackSize;
					return true;
				} else { // Otherwise, we already have a stack available to add to
					j.stackSize += i.stackSize; //Add the stack sizes
					lastAdded++;
					j.updatedOrder = lastAdded;
					currentWeight += j.weight * i.stackSize;
					return true;
				}
			}
		} else { //Otherwise, item is not stackable
			if (currentWeight + i.weight > maxWeight) {
				return false;
			} else {
				items.Add(i);
				lastAdded++;
				i.updatedOrder = lastAdded;
				currentWeight += i.weight;
				return true;
			}
		}
	}

	public Item FindItem(Item i) {
		return items.Find(i.CompareByName);
	}
	public void SortByName() { items.Sort(Item.CompareByName); }
	public void SortByRarity() { items.Sort(Item.CompareByRarity); }
	public void SortByValue() { items.Sort(Item.CompareByValue); }
	public void SortByWeight() { items.Sort(Item.CompareByWeight); }
	public void SortByObtained() { items.Sort(Item.CompareByUpdated); }

	public override string ToString() {
		string s = "Max weight = " + maxWeight +  ", Current weight = " + currentWeight + "\n";
		int x = 1;
		foreach(Item i in this.items) {
			s += x + ". " + i.ToString() + "\n";
			x++;
		}
		return s;
	}
}
