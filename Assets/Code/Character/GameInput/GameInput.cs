using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static float MoveX { get; private set; }
    public static float MoveZ { get; private set; }
    public static bool JumpBtnDown { get; private set; }
    public static bool JumpBtn { get; private set; }
    public static bool JumpBtnUp { get; private set; }
    public static bool PressedLeft => MoveX < -0.1f;
    public static bool PressedRight => MoveX > 0.1f;
    public static bool PressedDown => MoveZ < -0.1f;
    public static bool PressedUp => MoveZ < 0.1f;
    public static bool IsMoving => MoveX != 0 || MoveZ != 0;

    private void Update()
    {
        DirectionInputUpdate();
        ActionInputUpdate();
    }

    private void DirectionInputUpdate()
    {
        //LEFT - RIGHT
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveX = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveX = 1f;
        }
        else
        {
            MoveX = 0f;
        }

        //UP - DOWN
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveZ = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveZ = -1f;
        }
        else
        {
            MoveZ = 0f;
        }
    }

    private void ActionInputUpdate()
    {
        JumpBtnDown = Input.GetKeyDown(KeyCode.Space);
        JumpBtn = Input.GetKey(KeyCode.Space);
        JumpBtnUp = Input.GetKeyUp(KeyCode.Space);
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(20, 20, 500, 20), "UP " + KeyScheme.Up);
    //    GUI.Label(new Rect(20, 40, 500, 20), "DOWN " + KeyScheme.Down);
    //    GUI.Label(new Rect(20, 60, 500, 20), "Left " + KeyScheme.Left);
    //    GUI.Label(new Rect(20, 80, 500, 20), "Right " + KeyScheme.Right);
    //    GUI.Label(new Rect(20, 110, 500, 20), "MoveX " + MoveX);
    //    GUI.Label(new Rect(20, 130, 500, 20), "MoveY " + MoveY);
    //}
}