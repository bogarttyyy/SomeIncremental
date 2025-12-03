using UnityEngine;

public class EnvelopeSelect : MonoBehaviour
{
    [SerializeField] private Transform untrackedEnv;
    [SerializeField] private Transform trackedEnv;
    [SerializeField] private Transform expressEnv;

    [SerializeField] private Transform untrackedBig;
    [SerializeField] private Transform trackedBig;
    [SerializeField] private Transform expressBig;

    void Awake()
    {
        DisableAllBig();
        Select(untrackedBig);
    }

    public void OnUntrackedClicked()
    {
        Select(untrackedBig);
    }

    public void OnTrackedClicked()
    {
        Select(trackedBig);
    }

    public void OnExpressClicked()
    {
        Select(expressBig);
    }

    public void Select(EnvelopeType type)
    {
        switch (type)
        {
            case EnvelopeType.Untracked:
                Select(untrackedBig);
                break;
            case EnvelopeType.Tracked:
                Select(trackedBig);
                break;
            case EnvelopeType.Express:
                Select(expressBig);
                break;
        }
    }

    void Select(Transform toEnable)
    {
        DisableAllBig();
        if (toEnable != null)
        {
            toEnable.gameObject.SetActive(true);
        }
    }

    void DisableAllBig()
    {
        if (untrackedBig != null) untrackedBig.gameObject.SetActive(false);
        if (trackedBig != null) trackedBig.gameObject.SetActive(false);
        if (expressBig != null) expressBig.gameObject.SetActive(false);
    }
}
