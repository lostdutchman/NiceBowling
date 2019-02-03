using UnityEngine;
using System.Collections.Generic;

public class ActionMaster {
	public enum Action {Tidy, Reset, EndTurn, EndGame};

	private int[] bowls = new int[21];
	private int bowl = 1;

    public static Action NextAction (List<int> pinfFalls, int index, int currentPlayer, int numberOfPlayers) {
		ActionMaster am = new ActionMaster ();
		Action currentAction = new Action ();

        bool isLastTurn = false;
        if(currentPlayer == numberOfPlayers)
        {
            isLastTurn = true;
        }

        //Debug.Log(":::::Action Master Data:::::");
        //Debug.Log("Index: " + index + " IsLastTurn: " + isLastTurn.ToString());
        //string debug = "";
        //foreach (var number in pinfFalls)
        //{
        //    debug += number + ", ";
        //}
        //Debug.Log("Pins:" + debug);
        //Debug.Log("=======================================================");
        foreach (int pinFall in pinfFalls) {
			currentAction = am.Bowl (pinFall, index, isLastTurn);
		}
        return currentAction;
	}

    private Action Bowl (int pins, int index, bool isLastTurn) {
        if (pins < 0 || pins > 10) {Debug.Log ("Invalid pins passed to ActionMaster.Bowl" + pins); pins = 0;}

		bowls [bowl - 1] = pins;
        //string debug = "";
        //foreach (var number in bowls)
        //{
        //    debug += number + ", ";
        //}
        //Debug.Log("bowls:" + debug);
        //Debug.Log("bowl:" + bowl);

        if (bowl == 21) {
            if (isLastTurn)
            {
                return Action.EndGame;
            }
            else
            {
                return Action.EndTurn;
            }
        }

		// Handle last-frame special cases
		if (bowl >= 19 && pins == 10 ){
            bowl++;
			return Action.Reset;
		} else if (bowl == 20 ) {
            bowl++;
			if (bowls[19 - 1]==10 && bowls[20 - 1]==0) {
				return Action.Tidy;
			} else if (bowls[19 - 1] + bowls[20 - 1] == 10) {
				return Action.Reset;
			} else if ( Bowl21Awarded() ) {
				return Action.Tidy;
			} else {
                if (isLastTurn)
                {
                    return Action.EndGame;
                }
                else
                {
                    return Action.EndTurn;
                }
			}
		}

		if (bowl % 2 != 0) { // First bowl of frame
			if (pins == 10) {
                bowl += 2;
				return Action.EndTurn;
			} else {
                bowl += 1;
				return Action.Tidy;
			}
		} else if (bowl % 2 == 0) { // Second bowl of frame
            bowl += 1;
			return Action.EndTurn;
		}

		throw new UnityException ("Not sure what action to return!");
	}

	private bool Bowl21Awarded () {
		return (bowls [19-1] + bowls [20-1] >= 10);
	}
}
