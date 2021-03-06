﻿using UnityEngine;
using System.Collections;

public class TileValue  {

	public double value;
	Tile single;
	Tile[] pair = new Tile[2];

	public TileValue() {}

	public TileValue(Tile a, double value) {
		pair [0] = a;
		pair [1] = null;
		this.value = value;
	}

	public TileValue(Tile a, Tile b, double value) {
		pair[0] = a;
		pair[1] = b;
		this.value = value;
	}

	public Tile getTile() {
		return pair [0];
	}
	
	public Tile[] getTiles() {
		return pair;
	}

	public void setTiles(Tile a, Tile b) {
		pair [0] = a;
		pair [1] = b;
	}
	public void inc() {
		value++;
	}

	public void inc(int a) {
		value += a;
	}


}
