using UnityEngine;

public class GoalManager : SingletonMonobehaviour<GoalManager>
{

    public void AddToScore(int points)
    {
        Debug.Log(points);
    }



}
