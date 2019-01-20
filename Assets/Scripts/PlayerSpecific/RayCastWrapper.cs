using UnityEngine;
using UnityEngine.EventSystems;

public class RayCastWrapper
{
    private RaycastHit _hit;
    private readonly Camera _camera;

    public RayCastWrapper(Camera camera)
    {
        _camera = camera;
    }

    public RaycastHit CastRayFromCamera(LayerMask? layerMask)
    {
        _hit = new RaycastHit();
        if (!IsMouseOverUi())
        {
            CameraToMousePosition(layerMask);
        }
        return _hit;
    }

    private void CameraToMousePosition(LayerMask? layerMask)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (layerMask != null)
        {
            Physics.Raycast(ray, out _hit, _camera.farClipPlane, (LayerMask)layerMask);
        } else
            Physics.Raycast(ray, out _hit, _camera.farClipPlane);

    }

    private bool IsMouseOverUi()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}
