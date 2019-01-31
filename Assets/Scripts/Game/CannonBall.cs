using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

[RequireComponent(typeof(Rigidbody2D))]
public class CannonBall : CannonBallBehavior
{
    [SerializeField] private float speed;

    public uint ID;

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
            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && networkObject.IsOwner)
        {
            Player player = col.GetComponent<Player>();
            Debug.Log("A cannon ball has hit a player with an id of " + player.networkObject.Owner.NetworkId + " our id is " + ID);
            if (player.networkObject.Owner.NetworkId != ID)
            {
                player.PlayerHit(ID);
                networkObject.Destroy();

            }
            else
            {
                
            }
            
        }
    }
}
