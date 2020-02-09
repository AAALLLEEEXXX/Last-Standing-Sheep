using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool _alive = false;
    private float radius = 0.1f;
    private float maxDistanceRay = 2.0f;

    public bool Alive
    {
        get { return _alive; }
        set { _alive = value; }
    }

    private void FixedUpdate()
    {
        if (GameController.gameController.GetState() == PlatformState.Lift)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, radius, Vector3.down, out hit, maxDistanceRay))
            {
                if (hit.transform.CompareTag("Grass"))
                {
                    Die();
                }    
            }
        }
    }

    public void Die()
    {
        Alive = false;
        GameMenu.gameMenu.ShowMenu(Alive);
        transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        Destroy(gameObject);
    }
}
