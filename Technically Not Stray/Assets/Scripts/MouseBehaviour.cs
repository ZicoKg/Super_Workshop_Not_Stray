using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    public float wanderSpeed;
    public float runSpeed;
    public float aggroDistance;
    private float loseAggroDistance;

    [Range(0,100)]
    public int idleChance;
    public float wanderTimer;
    private float wanderTimerLeft;

    public enum mouseState { idle, wander, flee };
    public mouseState state;

    private Rigidbody rb;

    private GameObject player;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        loseAggroDistance = aggroDistance * 1.2f;
        wanderTimerLeft = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("[DEBUG] Mouse state: (" + state + ") wanderTimerLeft: " + wanderTimerLeft);
        if (state.Equals(mouseState.wander))
        {
            //Check if player is close enough to change state
            if(Vector3.Distance(player.transform.position, transform.position) < aggroDistance)
            {
                state = mouseState.flee;
            }
            //Mouse moves towards current movement cycle direction
            else if (wanderTimerLeft > 0)
            {
                wanderTimerLeft -= Time.deltaTime;
                rb.velocity = transform.forward * wanderSpeed * Time.deltaTime;
            }
            //Mouse decides to be idle for 1 movement cycle
            else if (Random.Range(0, 100) < idleChance)
            {
                wanderTimerLeft = wanderTimer;
                rb.velocity = Vector3.zero;
                state = mouseState.idle;
            }
            //Mouse picks a new direction for 1 movement cycle
            else
            {
                transform.Rotate(transform.rotation.x, Random.Range(0, 360), transform.rotation.z);
                wanderTimerLeft = wanderTimer;
            }
        }
        else if (state.Equals(mouseState.idle))
        {
            //Check if player is close enough to change state
            if (Vector3.Distance(player.transform.position, transform.position) < aggroDistance)
            {
                state = mouseState.flee;
            }

            //Wait unti movement cycle is over
            wanderTimerLeft -= Time.deltaTime;
            if (wanderTimerLeft <= 0)
            {
                state = mouseState.wander;
            }
        }
        else if (state.Equals(mouseState.flee))
        {
            //Check if player is far enough to change state
            if (Vector3.Distance(player.transform.position, transform.position) > loseAggroDistance)
            {
                state = mouseState.wander;
            }
            else
            {
                Vector3 facing = transform.position - player.transform.position;
                facing.y = 0;
                Quaternion rotation = Quaternion.LookRotation(facing);
                transform.rotation = rotation;
                rb.velocity = transform.forward * runSpeed * Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("WOOPS");
        if (collision.gameObject.transform.tag == player.tag)
        {
            Debug.Log("Mouse has been caught by the player");
            gameObject.SetActive(false);
        }
    }
}
