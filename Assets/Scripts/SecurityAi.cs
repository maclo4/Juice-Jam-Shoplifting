using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Pathfinding;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

// ReSharper disable Unity.InefficientPropertyAccess

public class SecurityAi : MonoBehaviour
{
    public List<Transform> patrolPath;
    public GameObject patrolPathPrefab;
    private int currPatrolPath;
    private Transform playerTransform;
    
    
    //public float speed = 0f;
    [FormerlySerializedAs("maxSpeed")] 
    public float walkSpeed = 3f;
    public float chaseSpeed = 5f;
    //public float currVelocity;
    public Vector2 currVelocityV2;
    public float nextWaypointDistance = 1f;
    
    //How long a guard will chase you after they've lost sight of you
    public float chaseTime;
    private float currChaseTime;
    private bool chaseCoroutineRunning;
    public bool playerInLineOfSight;
    public EnemyVision enemyVision;

    private Path path;

    private int currentWaypoint;
    private Seeker seeker;
    public float smoothTime = 0.3F;
    
    private Rigidbody2D rb;
    private Animator animator;

    //Current state of the security guard
    private SecurityStates state;
    private static readonly int Scanning = Animator.StringToHash("Scanning");

    // Start is called before the first frame update
    void Start()
    {
        patrolPath = patrolPathPrefab.GetComponentsInChildren<Transform>().ToList();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        SetRandomPath();
        InvokeRepeating("UpdatePathToPlayer", 0f, .5f);
        state = SecurityStates.Walking;
    }

    void SetRandomPath()
    {
        if (seeker.IsDone())
        {
            currPatrolPath = Random.Range(0, patrolPath.Count);
            seeker.StartPath(transform.position, patrolPath[currPatrolPath].position, OnPathComplete);
        }
    }

    void SetPathToPlayer()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, playerTransform.position, OnPathComplete);
        }
    }
    void UpdatePathToPlayer()
    {
        if (seeker.IsDone() && state == SecurityStates.Chasing )
        {
            seeker.StartPath(transform.position, playerTransform.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    
    void Update()
    {
        enemyVision.SetOrigin(transform.position);
        //If the analog stick in in the middle
        /*if (rb.velocity.x < .05 && rb.velocity.x > -.05 && rb.velocity.y < .05 && rb.velocity.y > -.05)
        {
            animator.SetTrigger("Idle");
        }*/
        if (path == null || state == SecurityStates.Scanning || currentWaypoint >= path.vectorPath.Count) return;
        
        Vector3 directionMoving = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;;
        string directionString = "";
        
        if (directionMoving.x > .05)
        {
            directionString = "East";
        }
        else if (directionMoving.x < -.05)
        {
            directionString = "West";
        }

        if (directionMoving.y > .05)
        {
            directionString += " North";
        }
        else if (directionMoving.y < -.05)
        {
            directionString += " South";
        }
        
        if(directionString == "West" || directionString == "West North" ||  directionString == "West South" )
            animator.SetTrigger("MoveLeft");
        
        if(directionString == "East" || directionString == "East North" ||  directionString == "East South" )
            animator.SetTrigger("MoveRight");
        
        if(directionString == "South")
            animator.SetTrigger("MoveDown");
        
        if(directionString == "North")
            animator.SetTrigger("MoveUp");
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(state == SecurityStates.Walking)
        {
            FollowPath();
        }

        if (state == SecurityStates.Scanning)
        {
            ScanArea();
        }
        
        if (state == SecurityStates.Chasing)
        {
            Chase();
        }
    }
    

    private void FollowPath()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("Scanning");
            state = SecurityStates.Scanning;
            animator.SetTrigger(Scanning);
            
            //speed = 0;
            currentWaypoint = 0;
            return;
        }
        
        //Get direction to move in
        var direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;

        // Make object look in direction of next waypoint
        //var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //speed = Mathf.SmoothDamp(speed, walkSpeed, ref currVelocity, smoothTime);
        //var smoothDampVelocity = Vector2.SmoothDamp(
         //   rb.velocity, direction * walkSpeed, ref currVelocityV2, smoothTime);
        //rb.velocity = smoothDampVelocity;
        //rb.velocity = direction * walkSpeed;
        
        var force = direction * walkSpeed * Time.deltaTime;
        transform.position =  new Vector3(transform.position.x + force.x, transform.position.y + force.y,
            transform.position.z);

        
        var distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    
    
    private void ScanArea()
    {
    }

    private void Chase()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            //speed = 0;
            currentWaypoint = 0;
            return;
        }
        
        //Get direction to move in
        var direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;

        var force = direction * chaseSpeed * Time.deltaTime;
        transform.position =  new Vector3(transform.position.x + force.x, transform.position.y + force.y,
            transform.position.z);
        //RotateTowardsNextWayPoint();

       // var smoothDampVelocity = Vector2.SmoothDamp(
       //     rb.velocity, direction * walkSpeed, ref currVelocityV2, smoothTime);
       // rb.velocity = smoothDampVelocity;
        //speed = Mathf.SmoothDamp(speed, runSpeed, ref currVelocity, smoothTime);
        //var force = direction * runSpeed * Time.deltaTime;
        //rb.velocity = force;
        //transform.position =  new Vector3(transform.position.x + force.x, transform.position.y + force.y,
        //    transform.position.z);

        
        var distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void RotateTowardsNextWayPoint()
    {
        if (currentWaypoint < path.vectorPath.Count - 1)
        {
            var directionNextWaypoint = ((Vector2) path.vectorPath[currentWaypoint + 1] - rb.position).normalized;
            // Make object look in direction of next waypoint
            var angle = Mathf.Atan2(directionNextWaypoint.y, directionNextWaypoint.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (currentWaypoint == path.vectorPath.Count)
        {
            var directionNextWaypoint = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
            // Make object look in direction of next waypoint
            var angle = Mathf.Atan2(directionNextWaypoint.y, directionNextWaypoint.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    //Called by Scan animation at the end
    public void EndScanning()
    {
        if (state == SecurityStates.Chasing) return;
        //Return to walking
        currentWaypoint = 0;
        state = SecurityStates.Walking;

        SetRandomPath();
    }

    public void SetPlayerPathOnSight(Transform playerLocation)
    {
        if (state != SecurityStates.Chasing)
        {
            playerTransform = playerLocation;
            state = SecurityStates.Chasing;
            
            SetPathToPlayer();
        }
        
        if (state == SecurityStates.Chasing)
        {
            currChaseTime = chaseTime;
        }
    }

    public void StartChaseTimer()
    {
        if (chaseCoroutineRunning) return;
        StartCoroutine(ChaseTimer());
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || state == SecurityStates.Chasing || !playerInLineOfSight) return;

        playerTransform = other.transform;
        state = SecurityStates.Chasing;
        
        SetPathToPlayer();
    }*/

    /*void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("On trigger stay ");
        if (!other.CompareTag("Player")) return;
        if (state != SecurityStates.Chasing && playerInLineOfSight) return;
        {
            Debug.Log("On trigger stay set player path");
            playerTransform = other.transform;
            state = SecurityStates.Chasing;
            
            SetPathToPlayer();
        }
        
        if (state == SecurityStates.Chasing && playerInLineOfSight)
        {
            currChaseTime = chaseTime;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || chaseCoroutineRunning) return;
        
        StartCoroutine(ChaseTimer());
    }*/

    IEnumerator ChaseTimer()
    {
        chaseCoroutineRunning = true;
        
        while (currChaseTime > 0)
        {
            
            Debug.Log(currChaseTime);
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(.5f);
            currChaseTime -= .5f;
        }

        currChaseTime = chaseTime;
        
        state = SecurityStates.Scanning;
        animator.SetTrigger(Scanning);
            
        //speed = 0;
        currentWaypoint = 0;
        
        chaseCoroutineRunning = false;
    }
}
