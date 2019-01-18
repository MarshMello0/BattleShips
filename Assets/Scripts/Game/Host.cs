using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private void FixedUpdate()
    {
        Vector3 newPos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            newPos.y += movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            newPos.y -= movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            newPos.x -= movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            newPos.x += movementSpeed * Time.deltaTime;
        }

        transform.position = newPos;
    }
}
