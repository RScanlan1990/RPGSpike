using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    private Canvas _targetMaker;
    private Camera _cameraToFace;
    private Outline _targetedOutline;

    protected virtual void Start()
    {
        _targetedOutline = GetComponent<Outline>();
        _targetMaker = gameObject.GetComponentInChildren<Canvas>();
        var nameMarker = _targetMaker.gameObject.GetComponentInChildren<Text>();
        nameMarker.text = transform.name.Replace("(Clone)", "");
        _targetMaker.enabled = false;
    }

    private void Update()
    {
        if (_targetMaker.enabled)
        {
            _targetMaker.transform.LookAt(_cameraToFace.transform.position);
        }
    }

    public virtual void EnableTargetedMarker(Camera cameraToFace)
    {
        _targetMaker.enabled = true;
        _cameraToFace = cameraToFace;
        _targetedOutline.enabled = true;
    }

    public virtual void DisableTargetMarker()
    {
        if (_targetMaker != null)
        {
            _targetMaker.enabled = false;
        }
        
        _targetedOutline.enabled = false;
    }

}
