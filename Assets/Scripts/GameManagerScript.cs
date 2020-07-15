using UnityEngine;
using UnityEngine.UI;
using Tools;

public class GameManagerScript : MonoBehaviour
{
    #region public variables
    public Canvas GameOverCanvas;
    public Text YourScoreGUI;
    public Text TopScoreGUI;
    public Text ScoreTextGUI;
    [Space]
    public TopScore TopScoreObject;
    [Space]
    public BallScript Ball;
    [Space]
    public Transform DynamicObjectsParent;
    public SpriteRenderer TerrainGroundUp;
    public GameObject FlagPole;
    #endregion

    #region private variables
    private int score;
    private Collider2D flagpoleCollider;
    #endregion

    #region Unity methods
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        GameOverCanvas.WarnIfReferenceIsNull(gameObject);
        YourScoreGUI.WarnIfReferenceIsNull(gameObject);
        TopScoreGUI.WarnIfReferenceIsNull(gameObject);
        ScoreTextGUI.WarnIfReferenceIsNull(gameObject);

        TopScoreObject.WarnIfReferenceIsNull(gameObject);

        Ball.WarnIfReferenceIsNull(gameObject);

        DynamicObjectsParent.WarnIfReferenceIsNull(gameObject);
        TerrainGroundUp.WarnIfReferenceIsNull(gameObject);
        FlagPole.WarnIfReferenceIsNull(gameObject);

        if (Ball != null)
        {
            Ball.Collision += Ball_Collision;
        }

        if (FlagPole != null)
        {
            FlagPole = Instantiate(FlagPole, DynamicObjectsParent);
        }

        if (FlagPole != null)
        {
            flagpoleCollider = FlagPole.GetComponentInChildren<Collider2D>();
        }

        GenerateLevel();
    }

    private void OnDestroy()
    {
        if (Ball != null)
        {
            Ball.Collision -= Ball_Collision;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ball.IncrementKickingStrength();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Ball.Kick();
        }
    }
    #endregion

    #region public methods
    public void Restart()
    {
        ResetScore();
        Ball.SetInteractable(true);
        SwitchGameOverCanvas(false);
    }
    #endregion

    #region private methods
    private void GenerateLevel()
    {
        if (DynamicObjectsParent != null && FlagPole != null && TerrainGroundUp != null)
        {
            var xMin = TerrainGroundUp.bounds.center.x;
            var xMax = TerrainGroundUp.bounds.center.x + TerrainGroundUp.bounds.extents.x - flagpoleCollider.bounds.extents.x;

            FlagPole.transform.position = new Vector2(Random.Range(xMin, xMax), TerrainGroundUp.bounds.center.y);
        }
    }

    private void GameOver()
    {
        Ball.ResetSettings();
        Ball.SetInteractable(false);

        if (TopScoreObject != null)
        {
            TopScoreObject.SetTopScore(score);
            TopScoreGUI.SetText(TopScoreObject.GetScore());
        }
        YourScoreGUI.SetText(score);

        SwitchGameOverCanvas(true);
        ResetScore();
    }

    private void IncrementScore()
    {
        score++;

        ScoreTextGUI.SetText(score);
    }

    private void ResetScore()
    {
        score = 0;

        ScoreTextGUI.SetText(score);
    }

    private void SwitchGameOverCanvas(bool active)
    {
        if (GameOverCanvas != null)
        {
            GameOverCanvas.gameObject.SetActive(active);
        }
    }

    private void Ball_Collision(object sender, bool isHoleCollision)
    {
        if (isHoleCollision)
        {
            GenerateLevel();
            Ball.IncrementDirectionVectorMovementSpeed();
            IncrementScore();
        }
        else
        {
            GameOver();
        }
    }
    #endregion
}
