using UnityEngine;
using System.Collections;

public class TilePairs  {

	public double value;
	Tile[] pair = new Tiles[2];

	public TilePairs() {}

	public TilePairs(Tile a, Tile b, double value) {
		pair[0] = a;
		pair[1] = b;
		this.value = value;
	}

	public Tile[] tiles() {
		return pair;
	}
}
