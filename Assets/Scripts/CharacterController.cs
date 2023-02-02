using DefaultNamespace;
using UnityEngine;
// ReSharper disable Unity.InefficientPropertyAccess

[RequireComponent(typeof(PlayerInputs))]
public class CharacterController : MonoBehaviour
{
    
    [HideInInspector] public PlayerInputs inputs;
    private Animator animator;
    private static readonly int CaughtAnim = Animator.StringToHash("Caught");
    private bool caught;
    public CutsceneManager cutsceneManager;
    public NpcManager npcManager;
    private Rigidbody2D rb;

    private Vector2 currVelocity;
    public float decelerationTime;
    public float accelerationTime;
    public float stealth;
    public float maxSpeed;
    public float valueStolen;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = GetComponent<PlayerInputs>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (caught)
        {
            animator.SetTrigger(CaughtAnim);
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
        if (caught)
        {
            animator.SetTrigger(CaughtAnim);
            return;
        }

        if (Time.timeScale == 0) return;
        //If the analog stick in in the middle
        /*if (rb.velocity.x < .05 && rb.velocity.x > -.05 && rb.velocity.y < .05 && rb.velocity.y > -.05)
        {
            animator.SetTrigger("Idle");
        }*/
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
    public void StealItem(Item item)
    {
        npcManager.TrackItemStolen(item);   
    }
    public void Caught()
    {
        caught = true;
    }
}
