using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEndGame : MonoBehaviour
{
    public GameObject team1Wins;
    public GameObject team2Wins;
    // Update is called once per frame
    void Update()
    {
        IfTeam1Wins();
        IfTeam2Wins();
    }
    private void IfTeam1Wins()
    {
        if (GameObject.FindGameObjectsWithTag("Team2").Length == 0)
        {
            Time.timeScale = 0;
            team1Wins.SetActive(true);
        }
    }
    private void IfTeam2Wins()
    {
        if (GameObject.FindGameObjectsWithTag("Team1").Length == 0)
        {
            Time.timeScale = 0;
            team2Wins.SetActive(true);
        }
    }
}
