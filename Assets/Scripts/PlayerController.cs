using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;

    private float range = Mathf.Infinity;
    private NavMeshAgent _playerAgent;

    void Start()
    {
        _playerAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    { 
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            DispatchRay();
        }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            DispatchRay();
        }
#endif
    }

    void DispatchRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, range, whatCanBeClickedOn))
        {
            _playerAgent.SetDestination(hitInfo.point);
        }
    }
}
