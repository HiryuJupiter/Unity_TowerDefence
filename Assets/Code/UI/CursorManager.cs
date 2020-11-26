using UnityEngine;
using System.Collections;
public class CursorManager : MonoBehaviour
{
    #region Fields
    public Texture2D CursorUp;
    public Texture2D CursorDown;
    #endregion

    #region MonoBehaviour    
    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(CursorDown, Vector2.zero, CursorMode.Auto);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(CursorUp, Vector2.zero, CursorMode.Auto);
        }
    }
    #endregion
}