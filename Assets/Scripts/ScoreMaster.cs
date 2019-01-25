using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScoreMaster {

	//Returns a cumulitive list of scores
	public static List<int> ScoreCumulative (List<int> Bowls){
		List<int> cumulativeScores = new List<int>();
		int runningTotal = 0; //missing last frame

		foreach (int frameScore in ScoreFrames (Bowls)) {
			runningTotal += frameScore;
			cumulativeScores.Add (runningTotal);
		}
		return cumulativeScores;
	}


	//Return a list of individual frame scores
	public static List<int> ScoreFrames(List<int> Bowls){
		List<int> frames = new List<int>();

		for(int i = 1; i < Bowls.Count; i +=2){
			if(frames.Count == 10)										//Prevents 11th frame
				break;

			if (Bowls[i - 1] + Bowls [i] < 10)							//Normal 'open' frame, sum to less than 10. 
				frames.Add (Bowls [i-1] + Bowls[i]);

			if (Bowls.Count - i <= 1)									//Insufficient look-ahead, stops calculating spare/strike bonus until there is one more bowl
				break;

			if(Bowls[i-1] == 10){										//Strike
				i--;													//Strike frame has just one bowl
				frames.Add (10 + Bowls [i+1] + Bowls [i+2]);
			}else if (Bowls[i-1] + Bowls[i] == 10)						//Calculate Spare
				frames.Add (10 + Bowls[i+1]);

		
		}
		
		return frames;
	}
}
 