using System;
using System.Globalization;
using UnityEngine;
using Tools;

[CreateAssetMenu(fileName = "TopScore", menuName = "ScoreSystem/TopScore")]
public class TopScore : ScriptableObject
{
    #region private variables
    private Result topScoreResult;
    #endregion

    #region public methods
    public void SetTopScore(int score)
    {
        if (score > topScoreResult.Score)
        {
            topScoreResult.Score = score;
            topScoreResult.DateTime = DateTime.Now.ToString(new CultureInfo("pl-PL"));
        }
    }

    public int GetScore()
    {
        return topScoreResult.Score;
    }

    public string GetDateTime()
    {
        return topScoreResult.DateTime;
    }

    public void ResetTopScore()
    {
        topScoreResult.Score = 0;
        topScoreResult.DateTime = string.Empty;
    }
    #endregion
}
