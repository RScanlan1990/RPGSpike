using UnityEngine;
using UnityEngine.UI;

public class PlayerUiController : UiController
{
    public Image MouseImage;
    public GameObject Inventory;
    public GameObject Equipment;
    public Camera TargetCamera;
    public GameObject TargetPanel;
    public RectTransform TargetHealthBar;

    private Player _player;
    private GameObject _targetCamera;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
        {
        if (_player.HaveTarget() && _player.TargetIsKillable())
        {
            UpdateTargetCamera();
            UpdateTargetPanel();
        }
        else
        {
            ResetTargetPanel();
        }

        if (MouseImage != null)
        {
            UpdateMouseIconPosition();
        }
    }

    private void UpdateMouseIconPosition()
    {
        MouseImage.transform.position = Input.mousePosition;
    }

    public void SetMouseIcon(Sprite icon)
    {
        MouseImage.sprite = icon;
        MouseImage.enabled = icon != null;
    }

    public void ToggleInventory()
    {
        CloseAll();
        Inventory.SetActive(true);
    }

    private void CloseAll()
    {
        Equipment.SetActive(false);
        Inventory.SetActive(false);
    }

    public void ToggleEquipment()
    {
        CloseAll();
        Equipment.SetActive(true);
    }

    private void UpdateTargetCamera()
    {
        SetCameraPanelActive();
        CreateCamera();
        UpdateCameraPosition();
    }

    private void SetCameraPanelActive()
    {
        if (TargetPanel.activeSelf == false)
        {
            TargetPanel.SetActive(true);
        }
    }

    private void CreateCamera()
    {
        if (!HaveCamera())
        {
            _targetCamera = Instantiate(TargetCamera, _player.GetTargetCameraOffSet(), Quaternion.Euler(_player.GetTargetCameraRotation()), _player.GetTargetTransform()).gameObject;
        }
    }

    private bool HaveCamera()
    {
        return _targetCamera != null;
    }

    private void UpdateCameraPosition()
    {
        _targetCamera.transform.SetParent(_player.GetTargetTransform(), false);
        _targetCamera.transform.localPosition = _player.GetTargetCameraOffSet();
        _targetCamera.transform.localRotation = Quaternion.Euler(_player.GetTargetCameraRotation());
    }

    private void UpdateTargetPanel()
    {
        TargetPanel.SetActive(true);
        var localScale = TargetHealthBar.localScale;
        localScale.x = _player.GetTargetHealth();
        TargetHealthBar.localScale = localScale;
    }

    private void ResetTargetPanel()
    {
        TargetPanel.SetActive(false);
        DestroyCamera();
    }

    private void DestroyCamera()
    {
        if (HaveCamera())
        {
            Destroy(_targetCamera);
        }
    }

    public override void EnableTargetedMarker(Camera cameraToFace)
    {

    }
}