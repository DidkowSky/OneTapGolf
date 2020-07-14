using UnityEngine;
using Tools;

public class LevelManagerScript : MonoBehaviour
{
    #region public variables
    public BallScript Ball;
    [Space]
    public Transform DynamicObjectsParent;
    public SpriteRenderer TerrainGroundUp;
    public GameObject FlagPole;
    #endregion

    #region private variables
    private Collider2D flagpoleCollider;
    #endregion

    #region Unity methods
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
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

    private void Ball_Collision(object sender, bool isHoleCollision)
    {
        Debug.Log($"Ball_Collision: {isHoleCollision}");

        if (isHoleCollision)
        {
            GenerateLevel();
            Ball.IncrementDirectionVectorMovementSpeed();
            //TODO: increment points
        }
        else
        {
            Ball.ResetSettings();
            //TODO: game over, show resulst
        }
    }
    #endregion
}
