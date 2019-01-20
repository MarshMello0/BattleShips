using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
[RequireComponent(typeof(Rigidbody2D))]
public class CannonBall : CannonBallBehavior
{
    [SerializeField] private float speed;

    protected override void NetworkStart()
    {
        base.NetworkStart();
        networkObject.position = transform.position;
        networkObject.positionInterpolation.target = transform.position;
        networkObject.SnapInterpolations();
        if (networkObject.IsOwner)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * speed);
            StartCoroutine(Destory());
        }

        
    }
    private void Update()
    {
        if (networkObject == null)
            return;
        if (networkObject.IsOwner)
        {
            networkObject.position = transform.position;
        }
        else
        {
            transform.position = networkObject.position;
        }
    }

    IEnumerator Destory()
    {
        yield return new WaitForSeconds(5);
        networkObject.Destroy();
    }
}
