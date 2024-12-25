using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 hotspot = new Vector2(64, 64); // Adjust based on your texture size
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

    private void Start()
    {
        SetCustomCursor();
    }

    public void SetCustomCursor()
    {
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }
}
