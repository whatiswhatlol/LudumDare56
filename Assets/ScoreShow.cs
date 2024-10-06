using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreShow : MonoBehaviour
{
    public TMP_Text text;
    public void ShowScore()
    { 
    text.text = "Your Score: \n" + PlayerStats.Instance.Score;
    }

}
