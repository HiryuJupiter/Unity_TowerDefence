using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CursorManager : MonoBehaviour
{
    #region Fields

    private static EventSystem eventSystem;

    public static bool IsMouseOverUI { get; private set; }


    //public Texture2D CursorUp;
    //public Texture2D CursorDown;
    #endregion

    #region MonoBehaviour    
    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        //Checks if we are mover over UI element
        IsMouseOverUI = eventSystem.IsPointerOverGameObject();
    }
    #endregion

    void CursorSpriteChangeUpdate ()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Cursor.SetCursor(CursorDown, Vector2.zero, CursorMode.Auto);
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    Cursor.SetCursor(CursorUp, Vector2.zero, CursorMode.Auto);
        //}
    }
}