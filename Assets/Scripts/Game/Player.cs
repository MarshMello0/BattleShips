using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System;

public class Player : PlayerBehavior
{
    [Header("Controls")]
    public List<KeyCode> forwardKeys = new List<KeyCode>();
    public List<KeyCode> backwardsKeys = new List<KeyCode>();
    public List<KeyCode> leftKeys = new List<KeyCode>();
    public List<KeyCode> rightKeys = new List<KeyCode>();
    [Space]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Transform cannonBL, cannonBR, cannonFL, cannonFR;

    [SerializeField]
    private float cameraSmoothness;

    [SerializeField] private Transform forward;



    private Camera cam;

    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            cam = new GameObject("Camera").AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 1;
        }
        else
        {

        }
    }

    private void Update()
    {
        if (networkObject == null)
            return;
        if (networkObject.IsOwner)
        {
            PlayerMovement();
            UpdateNetwork();
            CameraFollow();
        }
        else
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            cannonBL.localRotation = networkObject.BLCannon;
            cannonBR.localRotation = networkObject.BRCannon;
            cannonFL.localRotation = networkObject.FLCannon;
            cannonFR.localRotation = networkObject.FRCannon;
        }
    }

    private void CameraFollow()
    {
        if (cam == null)
            return;
        float step = cameraSmoothness * Time.deltaTime;

        cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(transform.position.x, transform.position.y, -10), step);
    }

    private void PlayerMovement()
    {
        float step = movementSpeed * Time.deltaTime;
        Vector3 newPos = transform.position;
        
        foreach (KeyCode key in forwardKeys)
        {
            if (Input.GetKey(key))
            {
                transform.position = Vector2.MoveTowards(transform.position, forward.position, step);
            }
        }

        foreach (KeyCode key in backwardsKeys)
        {
            if (Input.GetKey(key))
            {
                transform.position = Vector2.MoveTowards(transform.position, forward.position, -step);
            }
        }

        foreach (KeyCode key in leftKeys)
        {
            if (Input.GetKey(key))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + rotationSpeed);
            }
        }

        foreach (KeyCode key in rightKeys)
        {
            if (Input.GetKey(key))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - rotationSpeed);
            }
        }

        //Debug.DrawLine(transform.position, forward.position);
        
    }

    private void UpdateNetwork()
    {
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
        networkObject.BLCannon = cannonBL.localRotation;
        networkObject.BRCannon = cannonBR.localRotation;
        networkObject.FLCannon = cannonFL.localRotation;
        networkObject.FRCannon = cannonFR.localRotation;
    }
    public override void SetPrefs(RpcArgs args)
    {
        throw new NotImplementedException();
    }
}
