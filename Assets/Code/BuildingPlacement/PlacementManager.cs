﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacementManager : MonoBehaviour
{
    //Lazy singleton
    public static PlacementManager Instance;

    //Exposed variables
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private BasicTower Tower1;
    [SerializeField] private BasicTower Tower2;
    [SerializeField] private TowerGhost ghost_Tower1;
    [SerializeField] private TowerGhost ghost_Tower2;

    //Look up
    private Dictionary<TowerTypes, BasicTower> towerLookup = new Dictionary<TowerTypes, BasicTower>();
    private Dictionary<TowerTypes, TowerGhost> ghostLookup = new Dictionary<TowerTypes, TowerGhost>();

    //References
    private UIRendererManager ui;
    private Transform camera;

    //Status
    private TowerTypes towerMode;
    private Transform platformTransform;
    private RaycastHit hit;
    private TowerGhost currentGhost;
    private bool canReceiveMouseClick = true;

    //Cache
    private Plane invisiblePlane = new Plane(Vector3.up, new Vector3(0, 1, 0));

    //property
    public bool IsInPlacementMode { get; private set; }

    private void Awake()
    {
        //Lazy Singleton
        Instance = this;

        //Refernce
        camera = Camera.main.transform;

        //Initialize
        towerLookup = new Dictionary<TowerTypes, BasicTower>()
        {
            {TowerTypes.Tower1, Tower1 },
            {TowerTypes.Tower2, Tower2 },
        };

        ghostLookup = new Dictionary<TowerTypes, TowerGhost>()
        {
            {TowerTypes.Tower1, ghost_Tower1 },
            {TowerTypes.Tower2, ghost_Tower2 },
        };
    }

    private void Start()
    {
        //Reference
        ui = UIRendererManager.Instance;
    }

    private void Update()
    {
        //If in placement mode, then update the placement logic. 
        if (IsInPlacementMode)
        {
            PlacementUpdate();
            if (PlayerPressesExitKey)
            {
                ExitPlacementMode();
            }
        }
    }

    #region Public
    //Public methods to provide hooks for UI buttons
    public void EnterMode_Tower1() => EnterPlacementMode(TowerTypes.Tower1);
    public void EnterMode_Tower2() => EnterPlacementMode(TowerTypes.Tower2);
    #endregion

    #region Private
    private void ExitPlacementMode()
    {
        //Ghost
        SetCurrentGhostVisibility(false);

        //UI
        ui.ExitTowerPlacementMode();

        //Status
        IsInPlacementMode = false;
        platformTransform = null;
        
    }

    private void EnterPlacementMode(TowerTypes mode)
    {
        StartCoroutine(DelayMouseClickDetection());
        //UI
        ui.TowerPlacementMode(mode);

        //Ghost 
        SetCurrentGhostVisibility(false);
        currentGhost = ghostLookup[mode];
        SetCurrentGhostVisibility(true);

        //Status
        IsInPlacementMode = true;
        towerMode = mode;
    }

    private void PlaceTower()
    {
        //Create a tower and then place it on a  platform
        BasicTower tower = Instantiate(towerLookup[towerMode], platformTransform.position, Quaternion.identity);
        platformTransform.GetComponent<Platform>().PlaceTower(tower);
    }
    #endregion

    private void PlacementUpdate()
    {
        //Mostly updates ghost position. Also for exiting mode after placing tower.
        if (HitsAnEmptyPlatform())
        {
            //If hits an empty platform, make the ghost image green and snap it to place
            platformTransform = hit.transform;
            currentGhost.SetPosition(platformTransform.position);
            SetGhostPlacementAvailability(true);

            if (ClickedMouseToPlaceTower)
            {
                PlaceTower();
                ExitPlacementMode();
            }
        }
        else if (HitsAnyCollider)
        {
            //If hits a collider, then display ghost above it
            platformTransform = null;
            currentGhost.SetPosition(hit.point);
            SetGhostPlacementAvailability(false);
        }
        else
        {
            //If hits nothing, then make if float in air
            platformTransform = null;
            currentGhost.SetPosition(MousePositionOnInvisiblePlane());
            SetGhostPlacementAvailability(false);
        }
    }

    #region Minor methods and helper properties
    //Expression body methods for self documenting code
    private bool PlayerPressesExitKey => (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1));

    private bool HitsPlatform => Physics.Raycast(CameraToMouseRay, out hit, 100f, platformLayer);

    private bool HitsAnyCollider => Physics.Raycast(CameraToMouseRay, out hit, 100f);

    private bool ClickedMouseToPlaceTower => canReceiveMouseClick && !CursorManager.IsMouseOverUI && Input.GetMouseButtonDown(0);

    private Ray CameraToMouseRay => Camera.main.ScreenPointToRay(Input.mousePosition);

    

    private bool HitsAnEmptyPlatform()
    {
        //If hits an empty platform, return true
        if (HitsPlatform)
        {
            if (!hit.transform.GetComponent<Platform>().HasTower)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator DelayMouseClickDetection ()
    {
        //After clicking on a UI button, do not allow detecting mouse click on the 
        //...same frame
        canReceiveMouseClick = false;
        yield return null;
        canReceiveMouseClick = true;
    }

    private void SetCurrentGhostVisibility(bool isVisible)
    {
        if (currentGhost != null)
        {
            currentGhost.SetVisibility(isVisible);
        }
    }

    private Vector3 MousePositionOnInvisiblePlane()
    {
        Ray ray = CameraToMouseRay;
        float distance;
        invisiblePlane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    private void SetGhostPlacementAvailability(bool canPlace)
    {
        if (currentGhost != null)
        {
            currentGhost.SetPlacementAvailability(canPlace);
        }
    }
    #endregion

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(20, 20, 200,  20), "mode: " + PlacementMode);
    //}
}