using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSaveZone : MonoBehaviour
{
	public float timeForUp = 0.5f;
	public bool platformMoveUp = false;
	public bool platformMoveDown = false;
	private float _speed;
	private float _time = 0f;

    public GameObject maskPlatform;
    public GameObject originalPlatform;

    private void Start()
	{
		_speed = 5f / timeForUp;
	}

	private void Update()
	{
		if (platformMoveUp)
		{
			MovePlatform(Vector3.up, ref platformMoveUp);
            //ChangeStatePlatform(true, false);
        }
		if (platformMoveDown)
		{
			MovePlatform(Vector3.down, ref platformMoveDown);
            //ChangeStatePlatform(false, true);
        }
	}
	//Function for moving platform up and down
	private void MovePlatform(Vector3 direction, ref bool finished)
	{
		transform.Translate(direction * Time.deltaTime * _speed);
		_time += Time.deltaTime;
		if (_time >= timeForUp)
		{
			_time = 0f;
			finished = !finished;
		}
	}

    void ChangeStatePlatform(bool activeMask, bool activeOrigin)
    {
        maskPlatform.SetActive(activeMask);
        originalPlatform.SetActive(activeOrigin);
    }
}
