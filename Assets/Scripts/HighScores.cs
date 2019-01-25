using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class HighScores {

    public int Score { get; set; }
    public PlayerScores PlayerScore { get; set; }
    private List<HighScores> highScores;

    public bool CheckForHighScoreSinglePlayer(int score)
    {
        GetHighScores();
        if(highScores.Count < 10)
        {
            return true;
        }
        else
        {
            foreach(var HS in highScores)
            {
                if(score > HS.Score)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public void ListHighScores()
    {
        GetHighScores();
        foreach(var HS in highScores)
        {
            Debug.Log("HighScores: " + HS.Score + " " + HS.PlayerScore.playerName);
        }
    }

    public void SetHighScore(List<HighScores> playerScores)
    {
        GetHighScores();
        foreach(var player in playerScores)
        {
            highScores.Add(player);
        }
        List<HighScores> SortedHighScores = highScores.OrderBy(o=>o.Score).ToList();
        if(highScores.Count > 10)
        {
            highScores = SortedHighScores.GetRange(0, 9);
        }
        else
        {
            highScores = SortedHighScores;
        }
        SaveHighScores();
    }

    private void GetHighScores()
    {
        FileStream fs = new FileStream("hs.dat", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, highScores);
        fs.Close();
    }

    private void SaveHighScores()
    {
        using (Stream stream = File.Open("hs.dat", FileMode.Open))
        {
            var bformatter = new BinaryFormatter();

            highScores = (List<HighScores>)bformatter.Deserialize(stream);
        }
    }
}

