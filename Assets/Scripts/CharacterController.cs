using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable Unity.InefficientPropertyAccess

[RequireComponent(typeof(PlayerInputs))]
public class CharacterController : MonoBehaviour
{
    
    [HideInInspector] public PlayerInputs inputs;
    private Animator animator;
    private static readonly int CaughtAnim = Animator.StringToHash("Caught");
    private bool caught;
    public bool teleporting;
    private Vector3 teleportDest;
    public CutsceneManager cutsceneManager;
    public NpcManager npcManager;
    public HudManager hudManager;
    private Rigidbody2D rb;
    [SerializeField] private FieldOfView fieldOfView;

    private Vector2 currVelocity;
    private Vector2 currTeleportVelocity;
    public float decelerationTime;
    public float accelerationTime;
    [FormerlySerializedAs("stealth")] public float visionRange;
    public float maxSpeed;
    public float valueStolen;

    private List<Item> inventory;
    private static readonly int Teleport = Animator.StringToHash("Teleport");
    private static readonly int EndTeleport = Animator.StringToHash("EndTeleport");

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = GetComponent<PlayerInputs>();
        animator = GetComponentInChildren<Animator>();
        inventory = new List<Item>();
        fieldOfView.SetViewDistance(visionRange);
        npcManager.SetVisionDistance(visionRange - .5f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (caught)
        {
            animator.SetTrigger(CaughtAnim);
            return;
        }
        if (teleporting)
        {
            return;
        }
        if (inputs.move.magnitude >= .1)
        {
            var smoothDampVelocity = Vector2.SmoothDamp(
                rb.velocity, inputs.move * maxSpeed, ref currVelocity, accelerationTime);
            rb.velocity = smoothDampVelocity;
        }
        else if (inputs.move.magnitude < .1)
        {
            var smoothDampVelocity = Vector2.SmoothDamp(
                rb.velocity, Vector2.zero, ref currVelocity, decelerationTime);
            rb.velocity = smoothDampVelocity;
        }
    }

    void Update()
    {
        fieldOfView.SetAimDirection(inputs.move.normalized);
        fieldOfView.SetOrigin(transform.position);
        
        if (caught)
        {
            animator.SetTrigger(CaughtAnim);
            return;
        }
        if (teleporting)
        {
            return;
        }

        if (Time.timeScale == 0) return;


        if (inputs.move.x < -.05 && Mathf.Abs(inputs.move.x) > Mathf.Abs(inputs.move.y))
        {
            animator.SetTrigger("MoveLeft");
        }
        if (inputs.move.x > .05 && Mathf.Abs(inputs.move.x) > Mathf.Abs(inputs.move.y))
        {
            animator.SetTrigger("MoveRight");
        }
        if (inputs.move.y > .05 && Mathf.Abs(inputs.move.y) > Mathf.Abs(inputs.move.x))
        {
            animator.SetTrigger("MoveUp");
        }
        if (inputs.move.y < -.05 && Mathf.Abs(inputs.move.y) > Mathf.Abs(inputs.move.x))
        {
            animator.SetTrigger("MoveDown");
        }
    }

    public void UseItem()
    {
        if (inventory.Count <= 0) return;

        StartCoroutine(ApplyItemEffect());
    }
    private IEnumerator ApplyItemEffect()
    {
        var item = inventory.Last();
        item.UseItem(this);
        
        inventory.Remove(item);
        hudManager.UpdateItemImages(inventory);
        
        yield return new WaitForSeconds(item.duration);
        
        item.RemoveItemEffects(this);
    }

    public void StealItem(Item item)
    {
        if (item.usable)
        {
            valueStolen += item.price;
            inventory.Add(item);
            hudManager.UpdateItemImages(inventory);
        }
        else
        {
            valueStolen += item.price;
        }
        
        npcManager.TrackItemStolen(item);   
    }
    public void Caught()
    {
        caught = true;
    }

    public void IncreaseVisionRange(float increase)
    {
        visionRange += increase;
        fieldOfView.SetViewDistance(visionRange); 
        npcManager.SetVisionDistance(visionRange - .2f);
    }

    public void BeginTeleportPlayer(Vector3 destinationTeleporter)
    {
        teleporting = true;
        animator.SetBool(Teleport, true);
        teleportDest = destinationTeleporter;
        rb.velocity = Vector2.zero;
        
    }

    public void TeleportPlayer()
    {
        Debug.Log("teleport");
        transform.position = teleportDest;
        animator.SetBool(Teleport, false);
    }

    /*public void DisablePlayerInputs()
    {
        inputs.enabled = false;
    }
    public void EnablePlayerInputs()
    {
        inputs.enabled = false;
    }*/
}
