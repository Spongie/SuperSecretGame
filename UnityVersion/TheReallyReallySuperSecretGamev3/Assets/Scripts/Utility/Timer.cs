using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	private float timeToRun;
	private float timeRunning;
	
	public Timer(float msToRun)
	{
		timeToRun = msToRun;
		timeRunning = 0;
	}
	
	public bool Done
	{
		get { return timeToRun < timeRunning; }
	}
	
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
	}
}
