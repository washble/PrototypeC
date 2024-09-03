using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    [SerializeField]
    private int setWidth = 1920;
    [SerializeField]
    private int setHeight = 1080;
    
    void Start()
    {
        SetResolution();
    }

    private void SetResolution()
    {
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight);
            Camera.main.rect = new Rect((1f - newWidth) * 0.5f, 0f, newWidth, 1f);
        }
        else
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);
            Camera.main.rect = new Rect(0f, (1f - newHeight) * 0.5f, 1f, newHeight);
        }
    }
}
