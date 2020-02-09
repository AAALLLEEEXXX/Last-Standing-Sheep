using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private bool _enemyAlive =true;

    public bool EnemyAlive
    {
        get { return _enemyAlive; }
        set { _enemyAlive = value; }
    }

    public void Die()
    {
        EnemyAlive = false;
        GetComponent<EnemySheep>().DisableAgent();
        transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        Destroy(gameObject,1f);
    }
}
