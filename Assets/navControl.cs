using UnityEngine;
using UnityEngine.AI;

public class navControl : MonoBehaviour
{
    public GameObject Target;
    bool isWalking = true;
    private NavMeshAgent agent;
    private Animator animator;
    public float animationSpeedMultiplier = 1f;
    public Transform TargetFacing;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        if (isWalking)
            agent.destination = Target.transform.position;
        else
            agent.destination = transform.position;

        
        float currentSpeed = agent.velocity.magnitude;
        animator.speed = 1f + (currentSpeed * animationSpeedMultiplier);
        if (TargetFacing != null)
        {
            Vector3 lookDir = TargetFacing.position - transform.position;
            lookDir.y = 0f; 
            if (lookDir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), 10f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("attacking");
        if (other.name == "Dragon")
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");

           
            animator.speed = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Dragon")
        {
            isWalking = true;
            animator.SetTrigger("WALK");
        }
    }


}
