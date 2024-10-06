using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Death;
    public GameObject Leaderboard;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToDeath()
    {
        Leaderboard.SetActive(false);
        Death.SetActive(true);
    }
    public void SwitchToLeaderBoard()
    {
        Leaderboard.SetActive(true);
        Death.SetActive(false);
    }
}
