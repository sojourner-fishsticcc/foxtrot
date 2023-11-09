using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrappleHook : MonoBehaviour
{
    [Header("External Object/Component Fields")] 
    [SerializeField] public Rigidbody2D grappleRigidbody;
    [SerializeField] public GameObject player;
    public bool isGrappled;
    Vector3 lastVelocity;
    Grapple grapple;
    private Transform currentGrappledTransform;
    public float maxRopeDistance;
    void Start()
    {
        print("grapple thrown!");
        isGrappled = false;
        grapple = player.GetComponent<Grapple>();
    }
    void Update()
    {
        lastVelocity = grappleRigidbody.velocity;
    }
    private void OnCollisionEnter2D(Collision2D hookHit)
    {
        // if grappleableStatic, kill all velocity and set grapple to be 'true'
        if (hookHit.gameObject.tag == "Grappleable")
        {
            isGrappled = true;
            grapple.isGrappled = true;
            grapple.isGrappling = false;
            grappleRigidbody.gravityScale = 0f;
            grappleRigidbody.velocity = Vector2.zero;
            //freezes position, updates necessary booleans

            currentGrappledTransform = grappleRigidbody.transform;
            maxRopeDistance = Vector2.Distance(currentGrappledTransform.position, player.transform.position);
            print(maxRopeDistance);
            grapple.maxRopeDistance = maxRopeDistance;
        }
        // if ungrappleableRock, reflect direction adn velocity, set velocity to 90% of previous
        if (hookHit.gameObject.tag == "Ungrappleable")
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, hookHit.contacts[0].normal);
            grappleRigidbody.velocity = direction * Mathf.Max(speed * 0.9f);
                    // spark effect can be triggered here?    
        }
    }
}
