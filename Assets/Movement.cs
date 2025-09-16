using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class Movement : Agent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Dust;
    public float moveSpeed = 3f;
    float rotationSpeed = 90f;
    Vector3 StartPosition;
    float prevDistance = 0;

    public override void OnEpisodeBegin()
    {
        Reset();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float currentDistance = Vector3.Distance(transform.localPosition, Dust.transform.localPosition);
        float Delta = prevDistance - currentDistance;

        if (prevDistance != 0)
        {
            AddReward(Delta * 0.01f);    
        }
        prevDistance = currentDistance;
        

        AddReward(-0.001f);
        //En Array med disreta actions. Två element med värde 0, 1, eller 2
        //Matchar med de actions man definierat i Beahiour Parameters skriptet
        var action = actions.DiscreteActions;

        if (action[0] == 1)
            MoveForward();
        if (action[0] == 2)
            MoveBackwards();
        if (action[1] == 1)
            TurnRight();
        if (action[1] == 2)
            TurnLeft();
    }
    void Start()
    {
        SpawnDust();
        StartPosition = transform.position;
        Reset();
    }

    // Update is called once per frame
    // void Update()
    // {

    //     if (Keyboard.current.wKey.isPressed)
    //         MoveForward();
    //     else if (Keyboard.current.sKey.isPressed)
    //         MoveBackwards();
    //     if (Keyboard.current.aKey.isPressed)
    //         TurnLeft();
    //     else if (Keyboard.current.dKey.isPressed)
    //         TurnRight();

    //     if (Keyboard.current.oKey.isPressed)
    //     {
    //         SpawnDust();
    //     }
    // }

    void Reset()
    {
        transform.position = StartPosition;
        SpawnDust();
    }

    void MoveForward()
    {
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }

    void MoveBackwards()
    {
        transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
    }

    void TurnRight()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void TurnLeft()
    {
        transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
    }

    void SpawnDust()
    {
        Dust.transform.localPosition = new Vector3(Random.Range(4.0f, -5.0f), Dust.transform.localPosition.y, Random.Range(-12.0f, -3.0f));
    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "walls")
        {
            AddReward(-1.0f);
            Debug.Log("fail");
            EndEpisode();
        }
        else if (collision.gameObject.tag == "scrap")
        {
            AddReward(1.0f);
            Debug.Log("succes");
            EndEpisode();
        }

    }
}
