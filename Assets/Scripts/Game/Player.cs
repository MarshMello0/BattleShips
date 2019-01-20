using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System;
using TMPro;

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
    private Transform cannonBLSpawn, cannonBRSpawn, cannonFLSpawn, cannonFRSpawn;

    [SerializeField]
    private float cameraSmoothness;

    [SerializeField] private Transform forward;

    [SerializeField] private TextMeshPro name;
    [SerializeField] private SpriteRenderer sails;

    private Camera cam;

    protected override void NetworkStart()
    {
        base.NetworkStart();
        if (networkObject.IsOwner)
        {
            cam = new GameObject("Camera").AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 1;
            string username = PlayerPrefs.GetString("username");
            Color sailColour = new Color(PlayerPrefs.GetFloat("r"), PlayerPrefs.GetFloat("g"), PlayerPrefs.GetFloat("b"));
            networkObject.SendRpc(RPC_SET_PREFS, Receivers.AllBuffered, username, sailColour);
        }
        else
        {

        }
    }

    private void Update()
    {
        name.transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z);
        if (networkObject == null)
            return;
        if (networkObject.IsOwner)
        {
            PlayerMovement();
            CannonMovement();
            CannonShooting();
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

    private void CannonMovement()
    {
        if (cam == null)
            return;

        Vector2 mousePos = (Vector2)cam.ScreenToViewportPoint(Input.mousePosition);

        float LeftDis = Vector2.Distance(mousePos, W2V(cannonBL.position)) + Vector2.Distance(mousePos, W2V(cannonFL.position));
        float RightDis = Vector2.Distance(mousePos, W2V(cannonBR.position)) + Vector2.Distance(mousePos, W2V(cannonFR.position));

        if (LeftDis > RightDis)
        {
            Vector2 cannonFRScreen = cam.WorldToViewportPoint(cannonFR.position);
            Vector2 cannonBRScreen = cam.WorldToViewportPoint(cannonBR.position);

            float frontAngle = PosToAngle(mousePos, cannonFRScreen);
            float backAngle = PosToAngle(mousePos, cannonBRScreen);

            cannonFR.rotation = Quaternion.Euler(0, 0, frontAngle);
            cannonBR.rotation = Quaternion.Euler(0, 0, backAngle);

            cannonFR.localEulerAngles = new Vector3(cannonFR.localEulerAngles.x, cannonFR.localEulerAngles.y, ClampReset(cannonFR.localEulerAngles.z, 140f, 220f, -180f));
            cannonBR.localEulerAngles = new Vector3(cannonBR.localEulerAngles.x, cannonBR.localEulerAngles.y, ClampReset(cannonBR.localEulerAngles.z, 140f, 220f, -180f));
        }
        else if (RightDis > LeftDis)
        {
            Vector2 frontScreen = cam.WorldToViewportPoint(cannonFL.position);
            Vector2 backScreen = cam.WorldToViewportPoint(cannonBL.position);

            float frontAngle = PosToAngle(mousePos, frontScreen);
            float backAngle = PosToAngle(mousePos, backScreen);

            cannonFL.rotation = Quaternion.Euler(0, 0, frontAngle);
            cannonBL.rotation = Quaternion.Euler(0, 0, backAngle);

            cannonFL.localEulerAngles = new Vector3(cannonFL.localEulerAngles.x, cannonFL.localEulerAngles.y, ClampAngle(cannonFL.localEulerAngles.z, 310f,40f));
            cannonBL.localEulerAngles = new Vector3(cannonBL.localEulerAngles.x, cannonBL.localEulerAngles.y, ClampAngle(cannonBL.localEulerAngles.z, 310f, 40f));
        }
    }

    private void CannonShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            networkObject.SendRpc(RPC_SHOOT, Receivers.Server, cannonFLSpawn.position, cannonFL.eulerAngles.z);
            networkObject.SendRpc(RPC_SHOOT, Receivers.Server, cannonBLSpawn.position, cannonBL.eulerAngles.z);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            networkObject.SendRpc(RPC_SHOOT, Receivers.Server, cannonFRSpawn.position, cannonFR.eulerAngles.z);
            networkObject.SendRpc(RPC_SHOOT, Receivers.Server, cannonBRSpawn.position, cannonBR.eulerAngles.z);
        }
    }
    private float PosToAngle(Vector3 pos1, Vector3 pos2)
    {
        return Mathf.Atan2(pos1.y - pos2.y, pos1.x - pos2.x) * Mathf.Rad2Deg;
    }

    private Vector3 W2V(Vector3 pos)
    {
        return cam.WorldToViewportPoint(pos);
    }

    private float ClampAngle(float angle, float from, float to)
    {
        if (angle > from || angle < to)
        {
            return angle;
        }
        else
        {
            return 0;
        }
    }

    private float ClampReset(float angle, float from, float to, float reset)
    {
        if (angle > from && angle < to)
        {
            return angle;
        }
        else
        {
            return reset;
        }
    }

    public override void SetPrefs(RpcArgs args)
    {
        string username = args.GetNext<string>();
        Color sailColour = args.GetNext<Color>();
        name.text = username;
        sails.color = sailColour;
    }

    public override void Shoot(RpcArgs args)
    {
        if (networkObject.IsServer)
        {
            Vector3 pos = args.GetNext<Vector3>();
            float dir = args.GetNext<float>();
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Shoot(pos, dir);
        }
    }
}
