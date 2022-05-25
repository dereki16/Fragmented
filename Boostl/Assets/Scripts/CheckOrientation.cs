using UnityEngine;

public class CheckOrientation : MonoBehaviour
{
    public static bool portraitMode;
    public GameObject boostCanvas;
    public Transform canvasTransform;

    public void Check()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            portraitMode = true;
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            portraitMode = false;
        }
    }

    private void Awake()
    {
        Check();
    }

    public void Update()
    {
        Check();
        if (portraitMode)
            boostCanvas.transform.position = new Vector3(14.4f, 3.1f, 110f);
    }
}
