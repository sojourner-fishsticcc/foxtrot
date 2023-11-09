using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [Header("External Object/Component Fields")]
    [SerializeField] public GameObject Player;
    [SerializeField] public Rigidbody2D MercyRigidbody2D;
    [SerializeField] public GameObject Hook;
    [SerializeField] public GameObject PlayerGrapplingGun;
    [SerializeField] public LineRenderer LineRenderer;
    public Camera cam;
    private GameObject currentHook;
    private GameObject currentHookRotationPoint;
    private GameObject currentHookRotationSwing;
    private Vector2 PlayerGunDirection;
    public bool isGrappling;
    public bool isGrappled;
    private bool hasConvertedVelocity;
    public float maxRopeDistance;
    public float currentRopeDistance;
    private float CurrentAngularVelocity;
    private Vector2 ropeSwing;
    GrappleHook grappleHook;
    [SerializeField] public GameObject movementVector;


    void Start()
    {
        isGrappling = false;
        isGrappled = false;
    }
    void Update()
    {
        // creates and fires hook in the direction the player is looking, begins renderer script
        if (Input.GetButtonDown("Fire Hook"))
        {
        CreateHook();
        }
        // if player releases hook
        if (Input.GetButtonUp("Fire Hook"))
        {
        RetractHook();
        }
        // when player presses RMB while hook is down
        {
        HookFling();
        }
        if (isGrappling || isGrappled)
        {
        RenderRope();
        }
        if (isGrappled)
        {
        DistanceCheck();
        }
    // primary functions - the main functions of the ropes
    }
    public void CreateHook()
    {
        isGrappling = true;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)PlayerGrapplingGun.transform.position);
        direction.Normalize();
        direction *= 30;

        currentHook = Instantiate(Hook, PlayerGrapplingGun.transform.position, PlayerGrapplingGun.transform.rotation) as GameObject;
        currentHook.GetComponent<Rigidbody2D>().velocity = MercyRigidbody2D.velocity + direction;
        grappleHook = currentHook.GetComponent<GrappleHook>();
        Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), currentHook.GetComponent<Collider2D>(), true);
    }
    public void RetractHook()
    {
        Destroy(currentHook);
        isGrappling = false;
        isGrappled = false;
        LineRenderer.enabled = false;
    }
    public void TenseRope()
    {

    }
    public void HookFling()
    {

    }
    // secondary functions - functions that other primary functions rely on
    public void RenderRope()
    {
        LineRenderer.enabled = true;
        LineRenderer.SetPosition (0, MercyRigidbody2D.transform.position);
        LineRenderer.SetPosition (1, currentHook.transform.position);
    }
    public void DistanceCheck()
    {
        currentRopeDistance = Vector2.Distance(currentHook.transform.position, MercyRigidbody2D.transform.position);
        if (currentRopeDistance >= maxRopeDistance)
        {
            TenseRope();
        }
    }
}
