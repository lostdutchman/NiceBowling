using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScores{

    public int player { get; set; }
    private string _playerName;
    public string playerName
    {
        get
        {
            return _playerName;
        }
        set
        {
            if (value.Length <= 8)
            {
                _playerName = value;
            }
            else
            {
                _playerName = value.Substring(0, 7);
            }
        }
    }
    public List<int> bowls { get; set; }
    public Text[] bowlTexts { get; set; }
    public Text[] frameTexts { get; set; }
    public GameObject ScoreBoard { get; set; }
}
