using UnityEngine;

public class Interactable : Targetable
{
    public Vector3 TargetCameraOffset;
    public Vector3 TargetCameraRotation;

    public virtual void Interact(GameObject player)
    {
        //Override This Method with other classes..
    }
}

