using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterCollision : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidbody;
    [SerializeField] private float pushForce = 20f;
    NavMeshAgent agent;

    void Awake() 
    {
        agent = GetComponentInParent<NavMeshAgent>();
        myRigidbody = GetComponentInParent<Rigidbody>();
    }


    void OnTriggerEnter(Collider other) 
    {
        if(agent != null)
        {
            other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            agent.isStopped = true;
            Invoke("UnfreezeMyNavmeshAgent", 0.4f);
        }
        other.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        Invoke("UnfreezeMyRigidbody", 0.4f);
        Rigidbody impactedRb = other.gameObject.GetComponent<Rigidbody>();
        if (impactedRb == null) return;

        impactedRb.AddForce(transform.forward * (pushForce * 1.5f), ForceMode.Impulse);
    }

    private void UnfreezeMyRigidbody()
    {
        // Unfreeze the myRigidbody after 1 second
        myRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
        myRigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    private void UnfreezeMyNavmeshAgent()
    {
        agent.isStopped = false;
    }
    

    
}
