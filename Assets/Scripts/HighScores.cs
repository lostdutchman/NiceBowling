using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class HighScores{

    public int Score { get; set; }
    public List<int> bowls { get; set; }
    public string playerName { get; set; }

    public bool CheckForHighScoreSinglePlayer(int score)
    {
        List<HighScores> highScores = GetHighScores();
        if(highScores.Count < 5)
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

    public void SetHighScore(List<HighScores> playerScores)
    {
        List<HighScores> highScores = GetHighScores();
        foreach(var player in playerScores)
        {
            highScores.Add(player);
        }
        List<HighScores> SortedHighScores = highScores.OrderByDescending(o=>o.Score).ToList();
        if(highScores.Count > 5)
        {
            highScores = SortedHighScores.GetRange(0, 4);
        }
        else
        {
            highScores = SortedHighScores;
        }
        SaveHighScores(highScores);
    }

    public List<HighScores> GetHighScores()
    {
        List<HighScores> highScores = new List<HighScores>();
        try
        {
            using (Stream stream = File.Open(Application.persistentDataPath + @"/Assets/hs.dat", FileMode.Open))
            {
                var bformatter = new BinaryFormatter();
                highScores = (List<HighScores>)bformatter.Deserialize(stream);
            }
        }
        catch
        {
            Debug.Log("Failed to read " + Application.persistentDataPath + @"/Assets/hs.dat");
        }
        return highScores;
    }

    private void SaveHighScores(List<HighScores> highScores)
    {
        if (!Directory.Exists(Application.persistentDataPath + @"/Assets"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + @"/Assets");
        }
        FileStream fs = new FileStream(Application.persistentDataPath + @"/Assets/hs.dat", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, highScores);
        fs.Close();
    }
}

