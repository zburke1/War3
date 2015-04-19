using System;

using System.Timers;

public static class Waiter
{
	private static Timer myTimer;
	public static bool wait = true;
	public static void StartTimer(double t)
	{
		wait = true;
		myTimer = new Timer ((float)t*1000);
		myTimer.Elapsed += OnTimerComplete;
		myTimer.Start ();
		while (wait) {
			//wait!
		}
	}
	
	private static void OnTimerComplete(System.Object source, ElapsedEventArgs e)
	{
		myTimer.Stop ();
		wait = false;
	}
}

