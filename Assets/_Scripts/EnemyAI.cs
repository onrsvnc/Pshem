using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public Transform character;
    NavMeshAgent agent;
    Rigidbody myRigidbody;
    NavMeshHit hit;

    List<Vector3> characterPositions = new List<Vector3>();
    Vector3 characterVelocity = Vector3.zero;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Start() 
    {
        
    }

    void Update()
    {   
        Fight();

        if(!agent.enabled)
        {
            Invoke("WakeUpAgent", 0.4f);
        }
    }

    private void Fight()
    {
        // Store the current position of the character
        Vector3 characterPosition = character.position;
        characterPositions.Add(characterPosition);

        // Use the stored character position data to predict the character's future movements
        Vector3 predictedCharacterPosition = PredictCharacterPosition(characterPosition);

        // Set the destination for the NavMesh Agent to be the predicted character position
        agent.destination = predictedCharacterPosition;
    }

    Vector3 PredictCharacterPosition(Vector3 currentCharacterPosition)
    {
        // Calculate the character's velocity based on the past few frames
        if (characterPositions.Count >= 2)
        {
            characterVelocity = (characterPositions[characterPositions.Count - 1] - characterPositions[characterPositions.Count - 2]) / Time.deltaTime;
        }

        // Use linear regression to predict the character's future position
        Vector3 predictedCharacterPosition = currentCharacterPosition + characterVelocity * Time.deltaTime;

        // Return the predicted character position
        return predictedCharacterPosition;
    }

    private void Flight()
    {
        Vector3 normalDirection = (character.position - transform.position).normalized;
        agent.SetDestination(transform.position - (normalDirection));

    }

    private void WakeUpAgent()
    {
        agent.enabled = true;
    }


}
