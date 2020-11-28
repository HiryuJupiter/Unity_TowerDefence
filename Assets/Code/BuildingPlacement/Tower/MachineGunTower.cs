using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MachineGunTower : BasicTower
{
    [Header("Machine Gun Specifics")]
    [SerializeField] private Transform gunHolder;
    [SerializeField] private LineRenderer bulletLine;
    [SerializeField] private Transform firePoint;

    private float resetTime = 0;
    private bool hasResetVisuals = false;

    protected override void RenderAttackVisuals()
    {
        DistanceAndDirection(out float distance, out Vector3 direction, gunHolder, TargetedEnemy.transform);
        gunHolder.rotation = Quaternion.LookRotation(direction);

        RenderBulletLine(firePoint);
    }

    protected override void Update()
    {
        base.Update();

        //detect if we have no enemy and that we haven't already reset the visuals
        if (!hasResetVisuals)
        {
            if (resetTime < fireRate)
            {
                resetTime += Time.deltaTime;
            }
            else
            {
                bulletLine.positionCount = 0;
                resetTime = 0;
                hasResetVisuals = true;
            }
        }
    }

    void RenderBulletLine(Transform _start)
    {
        bulletLine.positionCount = 2;
        bulletLine.SetPosition(0, _start.position);
        bulletLine.SetPosition(1, TargetedEnemy.transform.position);
        hasResetVisuals = false;
    }

    public static void DistanceAndDirection(out float _distance, out Vector3 _direction, Transform _from, Transform _to)
    {
        Vector3 heading = _to.position - _from.position;
        _direction = heading.normalized;
        _distance = heading.magnitude;
    }

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(20, 40, 500, 20), "TargetedEnemy: " + TargetedEnemy);
    //    if (TargetedEnemy != null)
    //    {
    //        GUI.Label(new Rect(20, 70, 500, 20), "TargetedEnemy. isalive: " + TargetedEnemy.Alive);

    //    }
    //}

    /*public static void DistanceAndDirection(out float _distance, out Vector3 _direction, Vector3 _from, Vector3 _to)
    {
        Vector3 heading = _to - _from;
        _distance = heading.magnitude;
        _direction = heading.normalized;
    }*/
}
