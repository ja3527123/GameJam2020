using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LagButt : ButtObj, IPointerDownHandler, IPointerUpHandler {
	public float F = 5;
	public bool OK;
	float StartF;

	public Void_FloatDel UpdateDel;
	public Void_GameObjectDel EndDel;

	public void OnPointerDown(PointerEventData eventData){
		StartF = Time.time;
		OK = true;
	}
	public void OnPointerUp(PointerEventData eventData){
		if (EndDel != null) EndDel (gameObject);
		OK = false;
	}

	void Update () {
		if (OK)
		{
			float n = (StartF + F) - Time.time;
			if (UpdateDel != null) UpdateDel(n);
			if (n < 0) {
				if (Source_Name != "") SourceCon.Play (Source_Name);
				if (Del != null) Del (gameObject);
				OK = false;
			}
		}
	}
}
