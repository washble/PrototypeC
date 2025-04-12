using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class UIManager : Singleton<UIManager>
{
#if UNITY_ANDROID
    [SerializeField] private Button dashButton;
    public Button DashButton => dashButton;

    private Vector2[] buttonUIArea;
#endif

    private void Start()
    {
#if UNITY_ANDROID
        CalButtonUIAreaPositions();
#endif
    }

#if UNITY_ANDROID
    private void CalButtonUIAreaPositions()
    {
        Image[] images = { dashButton.image };

        buttonUIArea = new Vector2[images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            RectTransform rectTransform = images[i].rectTransform;

            Vector2 rectPosition = rectTransform.position;
            Vector2 rectSize = rectTransform.sizeDelta;
            Vector2 rectPivot = rectTransform.pivot;
        
            buttonUIArea[i] = new Vector2(
                rectPosition.x - rectSize.x * rectPivot.x,
                rectPosition.y + rectSize.y * (1 - rectPivot.y)
            );    
        }
    }

    public bool CanJoystickTouch(Vector2 position)
    {
        for (int i = 0; i < buttonUIArea.Length; i++)
        {
            if (position.x > buttonUIArea[i].x && position.y < buttonUIArea[i].y)
            {
                return false;
            }    
        }
        return true;
    }
#endif
}
