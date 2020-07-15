using System;
using System.Globalization;
using UnityEngine;
using UnityEditor;
using Tools;

[CreateAssetMenu(fileName = "TopScore", menuName = "ScoreSystem/TopScore")]
public class TopScore : ScriptableObject
{
    #region private variables
    [SerializeField]
    private Result topScoreResult = new Result();
    #endregion

    #region public methods
    public void SetTopScore(int score)
    {
        if (score > topScoreResult.Score)
        {
            topScoreResult.Score = score;
            topScoreResult.DateTime = DateTime.Now.ToString(new CultureInfo("pl-PL"));
            EditorUtility.SetDirty(this);
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
        EditorUtility.SetDirty(this);
    }
    #endregion
}
