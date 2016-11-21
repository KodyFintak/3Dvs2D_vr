using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class Stat
{
	[SerializeField]
	private BarScript bar;

	[SerializeField]
	private float maxVal;
	[SerializeField]
	private float currentVal;

	

	public float CurrentVal
	{
		get
		{
			return currentVal;
		}

		set
		{
			this.currentVal = Mathf.Clamp(value, 0, MaxVal);
			bar.Value = currentVal;
		}
	}

	public float MaxVal
	{
		get
		{
			return maxVal;
		}

		set
		{
			this.maxVal = value;
			bar.MaxValue = maxVal;

		}
	}

	public void setBar(BarScript setMe){
		bar = setMe;
	}

	public void Initialize()
	{
		this.MaxVal = maxVal;
		this.CurrentVal = currentVal;
	}
}
		