using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerScores{

    public int player { get; set; }
    public List<int> bowls { get; set; }
    public Text[] bowlTexts { get; set; }
    public Text[] frameTexts { get; set; }
}
