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
		i = new Item(i);
		if (i.isStackable) { // If item can be stacked,
			if (currentWeight + (i.weight * i.stackSize) > maxWeight) { // Check if we can add the stack by weight
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
		} else { // Otherwise, item is not stackable
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

	// Returns item, removed from inventory or null
	public Item RemoveItems(Item i, int ammount) {
		Item itemInInventory = FindItem(i);
		if(itemInInventory == null) { // Not found
			return null;
		} else { // Item found
			if(ammount > itemInInventory.stackSize) { // Requesting to remove more than available
				return null;
			} else if(ammount == itemInInventory.stackSize) { // Requesting to remove all available
				items.Remove(i);
				currentWeight -= ammount * itemInInventory.weight;
				return itemInInventory;
			} else { // Request to remove less than available
				itemInInventory.stackSize -= ammount;
				currentWeight -= ammount * itemInInventory.weight;
				Item removedItem = new Item(itemInInventory); // Copy constructor
				removedItem.stackSize = ammount;
				return removedItem;
			}
		}
	}

	// Call to just remove 1 of an item, if it is stackable, it will return the WHOLE stack
	public Item RemoveItem(Item i) {
		Item itemInInventory = FindItem(i);
		if (itemInInventory == null) { // Not found
			return null;
		} else { // Found
			if (itemInInventory.isStackable) {
				items.Remove(i);
				currentWeight -= itemInInventory.stackSize * itemInInventory.weight;
				return new Item(itemInInventory); // Copy constructor
			} else {
				items.Remove(i);
				currentWeight -= itemInInventory.weight;
				return new Item(itemInInventory);
			}
		}
	}

	// Searches for item by name, returns item if found or null if not
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
