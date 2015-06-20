using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{

	public float TimeToRun;
	public float TimeRunning;
	
	public void Restart()
	{
		TimeRunning = 0;
	}

    public bool Done
    {
        get { return TimeToRun < TimeRunning; }
    }

	public void Restart(float piTimeToRun)
	{
		TimeToRun = piTimeToRun;
		TimeRunning = 0;
	}

	void Update () 
    {
		TimeRunning += Time.deltaTime;
	}
}
