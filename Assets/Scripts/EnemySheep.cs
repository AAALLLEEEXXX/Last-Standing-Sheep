using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySheep : MonoBehaviour
{
    private float offset = 0.45f;
    private NavMeshAgent _enemyAgent;
    private Vector3 _destination;
    private EnemyHealth _health;
    private PlatformSaveZone _platform;
    private GameController gameController;


    void Start()
    {
        _enemyAgent = GetComponent<NavMeshAgent>();
        _health = GetComponent<EnemyHealth>();
        SetNewPoint();
        gameController = GameController.gameController;
        _enemyAgent.Move(Vector3.zero);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.EnemyAlive)
            //Watch on current game state and move freely or stay on platform
            switch (gameController.GetState())
            {
                case PlatformState.Wait:
                    if (_enemyAgent.remainingDistance <= offset)
                    {
                        SetNewPoint();
                    }
                    break;
                case PlatformState.Lift:
                    _enemyAgent.enabled = false;
                    break;
            }
    }

    //Setting new destination point for navMeshAgent
    private void SetNewPoint()
    {
        _destination = gameController.GetRandomPointOnSurface();
        _enemyAgent.SetDestination(_destination);
    }

    //Cancel all moving and send sheep on platform
    public void SetPlatformPoint(Vector3 platformPoint)
    {
        _enemyAgent.ResetPath();
        _enemyAgent.SetDestination(platformPoint);
    }

    //Make our navMeshAgent alive again
    public void Release()
    {
        _enemyAgent.enabled = true;
        if (_health.EnemyAlive)
            SetNewPoint();
    }

    public void DisableAgent()
    {
        _enemyAgent.enabled = false;
        gameController.KillSheep(this);
    }
}
