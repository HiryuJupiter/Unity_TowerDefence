using UnityEngine;
//using System.Linq;
using System.Collections;

/*
 What is side nudge? It's when the player controller hits the side of a platform 
above it by just a tiny bit, and the game automatically nudges the player out of the
way.
 */

[RequireComponent(typeof(Collider))]
public class MotorRaycaster : MonoBehaviour
{
    public Transform feet;

    //Const
    private const float checkDist = 0.2f;
    
    //Cache
    private LayerMask groundLayer;


    private void Start()
    {
        groundLayer = CharacterSettings.instance.GroundLayer;
    }

    public bool OnGrounDcheck()
    {
        //Debug.DrawRay(feet.position, Vector3.down * checkDist, Color.red);
        return Physics.RaycastAll(feet.position, Vector3.down, checkDist, groundLayer).Length > 0;
    }
}


//float direction = facingRight ? 1f : -1f;
//bot = Physics2D.Raycast(new Vector2(direction * extentX, -extentY), Vector3.right * direction, CheckDistance);
//top = Physics2D.Raycast(new Vector2(direction * extentX, extentY), Vector3.right * direction, CheckDistance);
//Debug.DrawRay(new Vector2(direction * extentX, -extentY), Vector3.right * direction * CheckDistance, Color.green);
//Debug.DrawRay(new Vector2(direction * extentX,  extentY), Vector3.right * direction * CheckDistance, Color.blue);

//public bool IsAgainstWall(int facingSign)
//{
//    RaycastHit2D bot = Raycast(BR, Vector2.right * facingSign, checkDist, Color.green);
//    RaycastHit2D top = Raycast(TR_outer, Vector2.right * facingSign, checkDist, Color.blue);

//    return (bot && top) ? true : false;
//}