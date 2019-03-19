using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
//[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    //private CharacterController characterController;
    private Animator animator;

    public float playerHeight;

    [Header("Mouvement Variables")]
    public float moveSpeed = 30;
    public bool smooth;
    public float smoothSpeed;
    //[SerializeField]
    //private float turnSpeed = 3.5f;


    private Vector3 lastPosition;
    private float distanceTraveled;
    public bool didHit = true;
    private bool aiming = false;

    private PlayerStats playerstats;


    public delegate void ViewChangeDelegate();
    public static event ViewChangeDelegate viewChangeDelegate;

    //Gravity
    public float gravity = 2.5f;

    //Physics
    public LayerMask discludePlayer;

    //References 
    public SphereCollider sphereCol;

    //Movement Vectors
    private Vector3 velocity;
    private Vector3 move;
    private Vector3 vel;
    private void Awake()
    {
        playerstats = (PlayerStats)GetComponent(typeof(PlayerStats));
        //characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        Gravity();
        if (ObjectClick.objectClicked)
        {
            //didHit = false;   // IT GETS CALLED EVERYTIME, WONT WORK
            distanceTraveled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            //Rotate();
            if (distanceTraveled < playerstats.AP.BaseValue * 10f)
            {
                SimpleMove();
                FinalMove();
            }
            if (!didHit)
            {
                if (Input.GetKeyDown(KeyCode.R) && !aiming)
                {
                    Attack();
                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    aiming = !aiming;
                    
                    RangedAttack();
                }

            }
        }
        GroundChecking();
        CollisionCheck();
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        didHit = true;
    }
    void RangedAttack()
    {
        //Get to the aim view
        //Get to the aim control
        //then push R button or Attack button elli houa
        viewChangeDelegate();
        aiming = !aiming;
    }
   //void Movement()
   //{
   //    var horizontal = Input.GetAxis("Horizontal");
   //    var vertical = Input.GetAxis("Vertical");
   //
   //    var movement = new Vector3(horizontal, 0, vertical);
   //    movement = transform.TransformDirection(movement);
   //
   //    //characterController.SimpleMove(movement * Time.deltaTime * moveSpeed);
   //}
    #region Movement Methods
    private void SimpleMove()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        velocity += move;
    }

    private void FinalMove()
    {
        vel = new Vector3(velocity.x, velocity.y, velocity.z) * moveSpeed;
        vel = transform.TransformDirection(vel);
        transform.position += vel * Time.deltaTime;

        velocity = Vector3.zero;
    }
    #endregion

    #region Gravity Stuff
    //Gravity Private Vars
    private bool grounded;

    //Grounded Private Vars
    private Vector3 liftPoint = new Vector3(0, 1.2f, 0);
    private RaycastHit groundHit;
    private Vector3 groundCheckPoint = new Vector3(0, -0.87f, 0);

    private void Gravity()
    {
        if (grounded == false)
        {
            velocity.y -= gravity;
        }
    }

    private void GroundChecking()
    {
        Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
        RaycastHit tempHit = new RaycastHit();

        if (Physics.SphereCast(ray, 0.17f, out tempHit, 20, discludePlayer))
        {
            GroundConfirm(tempHit);
        }else
        {
            grounded = false;
        }
    }

    private void GroundConfirm(RaycastHit tempHit)
    {
        Collider[] col = new Collider[3];
        int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(groundCheckPoint), 0.55f, col, discludePlayer);

        grounded = false;

        for (int i =0; i<num; i++)
        {
            if (col[i].transform == tempHit.transform)
            {
                groundHit = tempHit;
                grounded = true;

                //Snapping
                Vector3 avg = new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2), transform.position.z);
                if (!smooth)
                {
                    transform.position = avg;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2), transform.position.z),(smoothSpeed)*Time.deltaTime);
                }

                break;
            }

        }

        if (num <=1 && tempHit.distance <= 3.1f)
        {
            if (col[0] != null)
            {
                Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray,out hit,3.1f, discludePlayer))
                {
                    if (hit.transform!= col[0].transform)
                    {
                        grounded = false;
                        return;
                    }
                }
            }
        }
    }
    #endregion

    #region Collision Stuff
    private void CollisionCheck()
    {
        Collider[] overlaps = new Collider[4];
        Collider myCollider = new Collider();
        int num = 0;
        if (sphereCol != null)
        {
            num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(sphereCol.center), sphereCol.radius, overlaps, discludePlayer, QueryTriggerInteraction.UseGlobal);
            myCollider = sphereCol;
        }
        for (int i =0; i< num; i++)
        {
            Transform t = overlaps[i].transform;
            Vector3 dir;
            float dist;

            if (Physics.ComputePenetration(myCollider, transform.position, transform.rotation, overlaps[i],t.position,t.rotation,out dir, out dist))
            {
                Vector3 penetrationVector = dir * dist;
                Vector3 velocityProjected = Vector3.Project(velocity, -dir);
                transform.position = transform.position + penetrationVector;
                vel -= velocityProjected;
            }
        }
    }
    #endregion
    //void Rotate()
    //{
    //    var horizontal = Input.GetAxis("Horizontal");
    //    var vertical = Input.GetAxis("Vertical");
    //
    //    var movement = new Vector3(horizontal, 0, vertical);
    //    movement = transform.TransformDirection(movement);
    //    if (movement.magnitude > 0)
    //    {
    //        Quaternion newDirection = Quaternion.LookRotation(movement);
    //
    //        transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
    //    }
    //}
}