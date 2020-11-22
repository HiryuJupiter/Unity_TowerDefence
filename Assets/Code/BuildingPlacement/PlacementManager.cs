using UnityEngine;
using System.Collections;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    public PlacementModes PlacementMode { get; private set; } = PlacementModes.None;
    bool IsInPlacementMode => PlacementMode != PlacementModes.None;


    UIRendererManager ui;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ui = UIRendererManager.Instance;
    }

    void Update()
    {
        if (IsInPlacementMode && (Input.GetKeyDown(KeyCode.Escape)))
        {
            ExitPlacementMode();
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

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200,  20), "mode: " + PlacementMode);

    }
}