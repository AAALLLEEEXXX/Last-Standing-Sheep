using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformState
{
    Wait,
    Attention,
    Lift,
    Downward
}

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    //All created sheeps
    private List<EnemySheep> enemySheeps = new List<EnemySheep>();

    //Saving platform for our sheeps
    [SerializeField] private GameObject platform;
    [SerializeField] private Vector2 platformSize;
    //All sheep will be instantiated parented to this GO
    public int sheepsCount;
    public Transform sheepContainer;
    public GameObject enemySheepPrefab;

    //Time for player attention and sheep movement time to point
    public float secondsForAttention = 3f;
    //Time for platform up
    public float secondsInAir = 3f;
    //Waiting time for new platform
    public float timing = 7f;
    public float yRise = 5f;
    
    //Global timer for game states
    private float _time = 0f;
    private PlatformState _currentState = PlatformState.Wait;
    private Vector3 _currentScale;
    //Alive or nor current player
    private PlayerHealth playerHP;
    //Move safe platform up and down
    private PlatformSaveZone _platformMove;

    private void Start()
    {
        //Save current scale of platform for downgrade it
        _currentScale = platform.transform.localScale;
        _platformMove = platform.GetComponent<PlatformSaveZone>();
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        CreateEnemySheeps();
        Time.timeScale = 1f;
    }

    void CreateEnemySheeps()
    {
        for (int i = 0; i < sheepsCount; i++)
        {
            GameObject newSheep = Instantiate(enemySheepPrefab, GetRandomPointOnSurface(), Quaternion.identity, sheepContainer) as GameObject;
            enemySheeps.Add(newSheep.GetComponent<EnemySheep>());
        }
    }

    void Update()
    {
        _time += Time.deltaTime;
        switch (_currentState)
        {
            case PlatformState.Wait:
                //Spawn platform when time for waiting is over
                if (_time >= timing)
                {
                    _time = 0f;
                    platform.transform.localScale = _currentScale;
                    platform.transform.position = new Vector3(Random.Range(-platformSize.x / 2 + _currentScale.x / 2, platformSize.x / 2 - _currentScale.x / 2), -_currentScale.y / 2f + 0.3f, Random.Range(-platformSize.y / 2 + _currentScale.z / 2, platformSize.y / 2 - _currentScale.z / 2));
                    platform.SetActive(true);
                    SendSheepsOnPlatform();
                    _currentState = PlatformState.Attention;
                }
                break;
            case PlatformState.Attention:
                //Raise platform up after time for attention is over
                if (_time >= secondsForAttention)
                {
                    _time = 0f;
                    _platformMove.platformMoveUp = true;
                    _currentState = PlatformState.Lift;
                }
                break;
            case PlatformState.Lift:
                //after a short break, lower the platform
                if (_time >= secondsInAir)
                {
                    _time = 0f;
                    _platformMove.platformMoveDown = true;
                    _currentState = PlatformState.Downward;
                }
                break;
            case PlatformState.Downward:
                //when platform on ground - release our sheeps
                if (_time > _platformMove.timeForUp)
                {
                    _time = 0f;
                    ReleaseSheeps();
                    _currentScale *= 0.9f;
                    platform.SetActive(false);
                    _currentState = PlatformState.Wait;
                }
                break;
        }
    }
    //Notify all our sheeps that platform is ready
    private void SendSheepsOnPlatform()
    {
        foreach (EnemySheep sheep in enemySheeps)
        {
            sheep.SetPlatformPoint(platform.transform.position);
        }
    }
    //Notife all our sheeps that they're free to go
    private void ReleaseSheeps()
    {
        foreach (EnemySheep sheep in enemySheeps)
        {
            sheep.Release();
        }
    }
    //Kill sheep when ufo take her or she didnt have time to enter the platform
    public void KillSheep(EnemySheep sheep)
    {
        enemySheeps.Remove(sheep);
        if (enemySheeps.Count == 0 && playerHP.Alive)
        {
            GameMenu.gameMenu.ShowMenu(false);
        }
    }

    //Get Random point on plane
    public Vector3 GetRandomPointOnSurface()
    {
        return new Vector3(Random.Range(-platformSize.x / 2f, platformSize.x / 2f), platform.transform.position.y, Random.Range(-platformSize.y / 2f, platformSize.y / 2f));
    }

    public PlatformState GetState()
    {
        return _currentState;
    }
}

