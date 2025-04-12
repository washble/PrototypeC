using UnityEngine;

/**
 * If you wanna mobile joystick in UnityEditor
 * Open Window > Analysis > Input Debug
 * Options Tap > Simulate Touch Input From Mouse or Pen => Check!!!
 */
public class VirtualJoystick : MonoBehaviour
{
#if UNITY_ANDROID
    private InputManager inputManager;
    private UIManager uiManager;
    
    private RectTransform joystickRectTransform;
    
    private void Awake()
    {
        inputManager = InputManager.Instance;
        uiManager = UIManager.Instance;

        joystickRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += TouchStart;
        inputManager.OnEndTouch += TouchEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= TouchStart;
        inputManager.OnEndTouch -= TouchEnd;
    }

    private void TouchStart(Vector2 position, float time)
    {
        if (!uiManager.CanJoystickTouch(position)) return;
        
        joystickRectTransform.position = new Vector3(position.x, position.y, 0);
    }
    
    private void TouchEnd(Vector2 position, float time)
    {
        joystickRectTransform.position = new Vector3(10000, 10000, 0);
    }
#endif
}
