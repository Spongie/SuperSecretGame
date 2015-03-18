using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float timeToRun;
	public float timeRunning;
    public bool Done;
	
	public void Restart()
	{
		timeRunning = 0;
	}

	public void Restart(float piTimeToRun)
	{
		timeToRun = piTimeToRun;
		timeRunning = 0;
	}

	void Update () 
    {
		timeRunning += Time.deltaTime;
        Done = timeToRun < timeRunning;
	}
}
