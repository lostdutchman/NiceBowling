using UnityEngine;
using System.Collections.Generic;

public class ActionMaster {
	public enum Action {Tidy, Reset, EndTurn, EndGame};

	private int[] bowls = new int[21];
	private int[] playerBowl = new int[6] {1, 1, 1, 1, 1, 1 };

	public static Action NextAction (List<int> pinfFalls, int index) {
		ActionMaster am = new ActionMaster ();
		Action currentAction = new Action ();
        MonoBehaviour.print("currentAction Pre Loop= " + currentAction.ToString());
        foreach (int pinFall in pinfFalls) {
			currentAction = am.Bowl (pinFall, index + 1);
            MonoBehaviour.print("currentAction In Loop = " + currentAction.ToString());
		}
        MonoBehaviour.print("Post Loop = " + currentAction.ToString());
        return currentAction;
	}

	private Action Bowl (int pins, int index) { 
		if (pins < 0 || pins > 10) {Debug.Log ("Invalid pins passed to ActionMaster.Bowl"); pins = 0;}
		bowls [playerBowl[index] - 1] = pins;

		if (playerBowl[index] == 21) {
			return Action.EndGame;
		}

		// Handle last-frame special cases
		if (playerBowl[index] >= 19 && pins == 10 ){
            playerBowl[index]++;
			return Action.Reset;
		} else if (playerBowl[index] == 20 ) {
            playerBowl[index]++;
			if (bowls[19-1]==10 && bowls[20-1]==0) {
				return Action.Tidy;
			} else if (bowls[19-1] + bowls[20-1] == 10) {
				return Action.Reset;
			} else if ( Bowl21Awarded() ) {
				return Action.Tidy;
			} else {
				return Action.EndGame;
			}
		}

		if (playerBowl[index] % 2 != 0) { // First bowl of frame
			if (pins == 10) {
                playerBowl[index] += 2;
				return Action.EndTurn;
			} else {
                playerBowl[index] += 1;
				return Action.Tidy;
			}
		} else if (playerBowl[index] % 2 == 0) { // Second bowl of frame
            playerBowl[index] += 1;
			return Action.EndTurn;
		}

		throw new UnityException ("Not sure what action to return!");
	}

	private bool Bowl21Awarded () {
		return (bowls [19-1] + bowls [20-1] >= 10);
	}
}
