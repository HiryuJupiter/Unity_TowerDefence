using UnityEngine;
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
    private const float checkDist = 0.07f;
    private const float sideNudgeDist = 0.2f;
    
    //Cache
    private LayerMask groundLayer;
    private Vector3 halfExtent;
    


    #region Properties
    public bool IsOnGround => OnGroundCheck();

    #endregion

    #region MonoBehavior
    private void Awake()
    {
        float half = GetComponent<Collider>().bounds.extents.x;
        halfExtent = new Vector3(half, half, half);
    }

    private void Start()
    {
        groundLayer = CharacterMotorSettings.instance.GroundLayer;
    }

    //private void OnDrawGizmos()
    //{
    //    RaycastHit hitinfo;
    //    bool hits = Physics.BoxCast(transform.position, halfExtent, Vector3.down, out hitinfo, transform.rotation, checkDist, groundLayer);
    //    Debug.DrawRay(transform.position, Vector3.right * 100f, Color.yellow);
    //    if (hits)
    //    {
    //        Debug.DrawRay(hitinfo.point, Vector3.right * 100f, Color.red);

    //        Gizmos.DrawCube(transform.position + Vector3.down * hitinfo.distance, halfExtent * 2f);
    //    }
    //}
    #endregion

    #region Collision checks
    private bool OnGroundCheck()
    {
        return Physics.BoxCast(feet.position, halfExtent, Vector3.down, transform.rotation, checkDist, groundLayer);
        //return Physics.BoxCast(feet.position, halfExtent, Vector3.down, out hitinfo, transform.rotation, checkDist, groundLayer);
    }
    #endregion

    #region Util
    private RaycastHit2D Raycast(Vector2 origin, Vector2 dir, float dist, LayerMask mask, Color color)
    {
        
        Debug.DrawRay(origin, dir * dist, color);
        return Physics2D.Raycast(origin, dir, dist, mask);
    }
    #endregion
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