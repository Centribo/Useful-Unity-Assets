using System;
using System.Collections;
using System.Collections.Generic;


public class Item {
	public enum Rarities {
		Common = 0,
		Normal = 1,
		Rare = 2,
		Epic = 3
	};

	public string name;
	public int updatedOrder;
	public int value;
	public int weight;
	public int rarity;
	public bool isStackable;
	public int stackSize;

	public Item(string name, int value = 0, int weight = 0, int rarity = (int)Item.Rarities.Common, bool isStackable = true, int stackSize = 1) {
		this.name = name;
		this.value = value;
		this.weight = weight;
		this.rarity = rarity;
		this.isStackable = isStackable;
		this.stackSize = stackSize;
	}

	public override string ToString() {
		string s = "";
		s += name + " x" + stackSize + ": ";
		s += ((Rarities)rarity).ToString() + ", ";
		s += "v=" + value + " ";
		s += "w=" + weight + "(" + (weight*stackSize) + ") ";
		return s;
	}

	// Compare methods for finding
	public bool CompareByName(Item x) {
		return this.name == x.name;
	}
	public bool CompareByName(string name) {
		return this.name == name;
	}

	// Compare methods for sorting
	public static int CompareByName(Item x, Item y) {
		return x.name.CompareTo(y.name);
	}

	public static int CompareByRarity(Item x, Item y) {
		int result = -1 * x.rarity.CompareTo(y.rarity);
		if(result == 0) {
			return Item.CompareByName(x, y);
		} else {
			return result;
		}
	}

	public static int CompareByValue(Item x, Item y) {
		int result = -1 * x.value.CompareTo(y.value);
		if (result == 0) {
			return Item.CompareByName(x, y);
		} else {
			return result;
		}
	}

	public static int CompareByWeight(Item x, Item y) {
		int result = -1 * (x.weight * x.stackSize).CompareTo(y.weight * y.stackSize);
		if (result == 0) {
			return Item.CompareByName(x, y);
		} else {
			return result;
		}
	}

	public static int CompareByUpdated(Item x, Item y) {
		return x.updatedOrder.CompareTo(y.updatedOrder) * -1;
	}
}