using UnityEngine;
using Tools;

public class FlagAnimatorScript : MonoBehaviour
{
    #region public variables
    public GameObject FirstFlag;
    public GameObject SecondFlag;
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        FirstFlag.WarnIfReferenceIsNull(gameObject);
        SecondFlag.WarnIfReferenceIsNull(gameObject);

        if (FirstFlag != null)
        {
            FirstFlag.SetActive(true);
        }

        if (SecondFlag != null)
        {
            SecondFlag.SetActive(false);
        }

        InvokeRepeating("Animate", 0, 0.25f);
    }

    private void OnDisable()
    {
        CancelInvoke("Animate");
    }
    #endregion

    #region private methods
    private void Animate()
    {
        if (FirstFlag != null)
        {
            FirstFlag.SetActive(!FirstFlag.activeSelf);
        }

        if (SecondFlag != null)
        {
            SecondFlag.SetActive(!SecondFlag.activeSelf);
        }
    }
    #endregion
}
