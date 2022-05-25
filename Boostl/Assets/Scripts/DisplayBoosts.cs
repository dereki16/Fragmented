using UnityEngine;

public class DisplayBoosts : MonoBehaviour
{
    [SerializeField]
    private Boost bst;
    [SerializeField]
    private GameObject boost1;
    [SerializeField]
    private GameObject boost2;
    [SerializeField]
    private GameObject boost3;

    void Update()
    {
        switch (bst.boostsPlayerHas)
        {
            case 0:
                boost1.SetActive(false);
                boost2.SetActive(false);
                boost3.SetActive(false);
                break;
            case 1:
                boost1.SetActive(true);
                break;
            case 2:
                boost2.SetActive(true);
                break;
            case 3:
                boost3.SetActive(true);
                break;
            default:
                break;
        }
    }
}
