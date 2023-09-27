using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable Unity.InefficientPropertyAccess

[RequireComponent(typeof(PlayerInputs))]
public class PlayerCharacterController : MonoBehaviour
{
    
    [HideInInspector] public PlayerInputs inputs;
    private Animator _animator;
    private static readonly int CaughtAnim = Animator.StringToHash("Caught");
    private bool _disablePlayerControl;
    private Vector3 _teleportDest;
    private LevelTransitionManager _levelTransitionManager;
    private NpcManager _npcManager;
    private HudManager _hudManager;
    private Rigidbody2D _rb;
    [SerializeField] private FieldOfView fieldOfView;

    private Vector2 _currVelocity;
    private Vector2 _currTeleportVelocity;
    public float decelerationTime;
    public float accelerationTime;
    [FormerlySerializedAs("stealth")] public float visionRange;
    public float maxSpeed;
    public float valueStolen;

    private List<Item> _inventory;
    private static readonly int Teleport = Animator.StringToHash("Teleport");
    private static readonly int MoveLeft = Animator.StringToHash("MoveLeft");
    private static readonly int MoveRight = Animator.StringToHash("MoveRight");
    private static readonly int MoveUp = Animator.StringToHash("MoveUp");
    private static readonly int MoveDown = Animator.StringToHash("MoveDown");

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        inputs = GetComponent<PlayerInputs>();
        _animator = GetComponentInChildren<Animator>();
        _hudManager = HudManager.Instance;
        _npcManager = NpcManager.Instance;
        _levelTransitionManager = LevelTransitionManager.Instance;
        
        _inventory = new List<Item>();
        fieldOfView.SetViewDistance(visionRange);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_disablePlayerControl)
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        if (inputs.move.magnitude >= .1)
        {
            var smoothDampVelocity = Vector2.SmoothDamp(
                _rb.velocity, inputs.move * maxSpeed, ref _currVelocity, accelerationTime);
            _rb.velocity = smoothDampVelocity;
        }
        else if (inputs.move.magnitude < .1)
        {
            var smoothDampVelocity = Vector2.SmoothDamp(
                _rb.velocity, Vector2.zero, ref _currVelocity, decelerationTime);
            _rb.velocity = smoothDampVelocity;
        }
    }

    void Update()
    {
        fieldOfView.SetAimDirection(inputs.move.normalized);
        fieldOfView.SetOrigin(transform.position);
        
        if (_disablePlayerControl)
        {
            return;
        }

        if (Time.timeScale == 0) return;


        if (inputs.move.x < -.05 && Mathf.Abs(inputs.move.x) > Mathf.Abs(inputs.move.y))
        {
            _animator.SetTrigger(MoveLeft);
        }
        if (inputs.move.x > .05 && Mathf.Abs(inputs.move.x) > Mathf.Abs(inputs.move.y))
        {
            _animator.SetTrigger(MoveRight);
        }
        if (inputs.move.y > .05 && Mathf.Abs(inputs.move.y) > Mathf.Abs(inputs.move.x))
        {
            _animator.SetTrigger(MoveUp);
        }
        if (inputs.move.y < -.05 && Mathf.Abs(inputs.move.y) > Mathf.Abs(inputs.move.x))
        {
            _animator.SetTrigger(MoveDown);
        }
    }

    public void UseItem()
    {
        if (_inventory.Count <= 0) return;

        StartCoroutine(ApplyItemEffect());
    }
    private IEnumerator ApplyItemEffect()
    {
        var item = _inventory.Last();
        item.UseItem(this);
        
        _inventory.Remove(item);
        _hudManager.UpdateItemImages(_inventory);
        
        yield return new WaitForSeconds(item.duration);
        
        item.RemoveItemEffects(this);
    }

    public void StealItem(Item item)
    {
        if (item.usable)
        {
            valueStolen += item.price;
            _inventory.Add(item);
            _hudManager.UpdateItemImages(_inventory);
        }
        else
        {
            valueStolen += item.price;
        }
        
        _npcManager.TrackItemStolen(item);   
    }
    public void Caught()
    {
        StartCoroutine(CaughtCoroutine());
    }

    private IEnumerator CaughtCoroutine()
    {
        _disablePlayerControl = true;
        _animator.SetTrigger(CaughtAnim);
        yield return new WaitForSeconds(.5f);
        _levelTransitionManager.LoadGameOverScreen();
    }

    public void IncreaseVisionRange(float increase)
    {
        visionRange += increase;
        fieldOfView.SetViewDistance(visionRange); 
    }

    public void BeginTeleportPlayer(Vector3 destinationTeleporter)
    {
        StartCoroutine(TeleportCoroutine(destinationTeleporter));
    }

    private IEnumerator TeleportCoroutine(Vector3 destinationTeleporter)
    {
        _animator.SetBool(Teleport, true);
        _rb.velocity = Vector2.zero;
        _disablePlayerControl = true;
        
        yield return new WaitForSeconds(.3f);
        
        TeleportPlayer(destinationTeleporter);
        _animator.SetBool(Teleport, false);
        
        yield return new WaitForSeconds(.3f);
        
        _disablePlayerControl = false;
    }

    public void TeleportPlayer(Vector3 destinationTeleporter)
    {
        Debug.Log("teleport");
        transform.position = destinationTeleporter;
        _animator.SetBool(Teleport, false);
    }

    public bool IsPlayerControlDisabled()
    {
        return _disablePlayerControl;
    }
}
