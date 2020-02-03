using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragObj : MonoBehaviour
{
    static public DragObj _;

    public DragButt Butt;
    public Transform Obj;
    public Rigidbody2D Rig2D;
    public Transform[] SpTs;

    public GameObject WinObj;
    public ButtObj ResetButt;

    public bool OK;
    public float TAng;
    public GameObject controller;
    public Vector3 SpPos
    {
        get
        {
            Transform r = null;
            for (int i = 0; i < SpTs.Length; i++)
            {
                if (r == null || SpTs [i].position.y > r.position.y)
                {
                    r = SpTs[i];
                }
            }
            return r.position;
        }
    }

    private void Awake()
    {
        _ = this;
    }

    private void Start()
    {
        Butt.Start_Del += (v3) =>
        {
            OK = true;
        };
        Butt.Update_Del += (v3) =>
        {
            Obj.position = v3;
        };
        Butt.End_Del += (v3) =>
        {
            OK = false;
        };

        ResetButt.Del += (g) =>
        {
            SceneManager.LoadScene("Main");
        };
    }
    private void Update()
    {
        if (OK)
        {
            Rig2D.velocity = (Butt.End_V3 - Rig2D.transform.position) * 20;

            //修改目標角度
            float MouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (MouseScrollWheel != 0)
            {
                TAng += MouseScrollWheel * 1000;
            }
        }
        else
        {
            Rig2D.velocity = Vector2.zero;
        }

        //控制角度旋轉
        //print(TAng - Rig2D.transform.eulerAngles.z);
        Rig2D.angularVelocity = TAng;
        TAng = Mathf.Lerp(TAng, 0, 0.2f);

        //================================test
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpObj(new GameObject(), transform.parent);
        }
    }

    public GameObject SpObj (GameObject Obj, Transform Parent)
    {
         return Instantiate(Obj, SpPos, Quaternion.identity, Parent);
    }
}
