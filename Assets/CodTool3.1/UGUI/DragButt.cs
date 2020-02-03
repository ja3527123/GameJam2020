using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragButt : ButtObj, IPointerDownHandler, IPointerUpHandler,IDragHandler {
	public Vector3 Start_V3, End_V3;
	public Vector3 Drag_V3 {
		get {
			return End_V3 - Start_V3;
		}
	}
	public Void_Vector3Del Start_Del, Update_Del, End_Del;
    Canvas canvas;

    void Start()
    {
        if (!canvas)
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        Start_V3 = GetWorldPot(eventData.position);
        End_V3   = Start_V3;
		if (Start_Del != null) Start_Del (Start_V3);
    }
	public void OnDrag(PointerEventData eventData){
        End_V3 = GetWorldPot(eventData.position);
		if (Update_Del != null) Update_Del (End_V3);
    }
	public void OnPointerUp(PointerEventData eventData){
		if (End_Del != null) End_Del (End_V3);
    }
    // 用ＵＩ做標取得世界座標
    public Vector3 GetWorldPot (Vector3 V3)
    {
        V3.x -= canvas.pixelRect.width  / 2;
        V3.y -= canvas.pixelRect.height / 2;
        return canvas.transform.TransformPoint(V3);
    }


	public Vector3 GetLocalV3 (Vector3 V3) {
		return (V3 - transform.position) / transform.lossyScale.x;
	}
}