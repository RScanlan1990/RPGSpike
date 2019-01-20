using UnityEditor;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    private UiController _uiController;
    public GUID TargetableId;

    protected virtual void Start()
    {
        _uiController = gameObject.GetComponent<UiController>();
        TargetableId = GUID.Generate();
    }

    public void GetTargeted(Camera cameraToFace)
    {
        _uiController.EnableTargetedMarker(cameraToFace);
    }

    public void GetUnTargeted()
    {
        _uiController.DisableTargetMarker();
    }
}
