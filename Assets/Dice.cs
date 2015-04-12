using UnityEngine;
using System;
using System.Collections;

public class Dice {
	private int[] dies = new int[3]; 
	public Dice() {}
	//returns a result of the number of armies lost for attacking and defending sides for this dice roll.
	public static int[] roll(int attackerArmies, int defenderArmies) {
		int[] attacking, defending;
		int[] result = {0,0};
		//set number of dice for attacker
		if (attackerArmies > 3) {
			attacking = new int[3];
		} else if (attackerArmies == 2) {
			attacking = new int[2];
		} else {
			attacking = new int[1];
		}
		//set number of dice for defender
		if (defenderArmies > 2) {
			defending = new int[3];
		} else if (defenderArmies == 2) {
			defending = new int[2];
		} else {
			defending = new int[1];
		}

		//throw dice.
		for (int i = 0; i < attacking.Length; i++) {
			//note: not sure about using UnityEngine.Random vs System.Random
			attacking[i] = UnityEngine.Random.Range (1, 7); //max is exclusive
		}
		for (int i = 0; i < defending.Length; i++) {
			//note: not sure about using UnityEngine.Random vs System.Random
			defending[i] = UnityEngine.Random.Range (1, 7); //max is exclusive
		}
		//sort arrays least to greatest.
		Array.Sort (attacking);
		Array.Sort (defending);

		//compare dice rolls.
		if (attacking.Length > defending.Length) {
			//start at the highest values, and move down
			for (int i = 0; i <defending.Length; i++) {
				if (attacking[attacking.Length-i] > defending[defending.Length-i]) {
					//attacking die is higher than defending die. defender -1.
					result[1]--;
				} else {
					//defending die is higher than attacking die. attacker -1.
					result[0]--;
				}
			} 
		} else {
			//start at the highest values, and move down
			for (int i = 0; i <attacking.Length; i++) {
				if (attacking[attacking.Length-i] > defending[defending.Length-i]) {
					//attacking die is higher than defending die. defender -1
					result[1]--;
				} else {
					//defending die is higher than attacking die. attacker -1.
					result[0]--;
				}
			} 
		}

		return result;
	}
}
