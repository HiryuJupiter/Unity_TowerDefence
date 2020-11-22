using UnityEngine;
using System.Collections;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    [SerializeField] Dummy ghost_Tower1;
    [SerializeField] Dummy Tower1;

    //References
    UIRendererManager ui;
    Transform camera;
    Settings settings;

    public PlacementModes PlacementMode { get; private set; } = PlacementModes.None;
    bool IsInPlacementMode => PlacementMode != PlacementModes.None;
    bool PlayerPressesExitKey => (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1));


    void Awake()
    {
        Instance = this;
        camera = Camera.main.transform;
    }

    void Start()
    {
        ui = UIRendererManager.Instance;
        settings = Settings.Instance;
    }

    void Update()
    {
        if (IsInPlacementMode)
        {
            PlacementUpdate();
            if (PlayerPressesExitKey)
            {
                ExitPlacementMode();
            }
        }
    }

    public void EnterMode_Tower1 () => SetPlacementMode(PlacementModes.Tower1);
    public void EnterMode_Tower2 () => SetPlacementMode(PlacementModes.Tower2);
    public void ExitPlacementMode() => SetPlacementMode(PlacementModes.None);

    public void SetPlacementMode(PlacementModes mode)
    {
        PlacementMode = mode;
        ui.RevealSpawningModeUI(mode);
    }

    void PlacementUpdate ()
    {

    }


    void MouseRaycast ()
    {
        if (Physics.Raycast(camera.position, camera.forward, 1000f, settings.PlatformLayer))
        {

        }
    }

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(20, 20, 200,  20), "mode: " + PlacementMode);
    //}
}