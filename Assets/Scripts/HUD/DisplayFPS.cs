﻿using UnityEngine;
using System.Collections;

public class DisplayFPS : MonoBehaviour
{
	[SerializeField] TextMesh textDisplay;
	public  float updateInterval = 0.5F;
	private float accum   = 0; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	
	void Start()
	{
		if(!textDisplay)
		{
			Debug.Log("Need to attach TextMesh component!");
			enabled = false;
			return;
		}
		timeleft = updateInterval;  
	}

	void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update TextMesh text and start new interval
		if(timeleft <= 0.0)
		{
			// display two fractional digits (f2 format)
			float fps = accum/frames;
			string format = System.String.Format("{0:F2} FPS",fps);
			textDisplay.text = format;
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0;
		}
	}
}