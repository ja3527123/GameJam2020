using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmanager : MonoBehaviour
{
    static public Mainmanager _;

    public DragObj DragObj;
    public Picemanager Picemanager;
    public ObjArray Pice_x;

    [Serializable]
    public struct NMod
    {
        public float Size;
        public string Key;
    }
    public NMod[] nMods;
    public float NowSize => nMods[n].Size;
    public string NowKey => nMods[n].Key;

    public triggergameobjec NowT;
    public int n = 0;

    private void Awake()
    {
        _ = this;
    }

    private void Start()
    {
        triggergameobjec Obj = DragObj.SpObj(Picemanager.triggergameobjecs[0].gameObject, transform).GetComponent<triggergameobjec>();
        Obj.Open(nMods[0].Key);
        LoadPice(Obj, 0);

        SetMod(0);
    }

    //private void Update()
    //{
    //    Pice_x.transform.localScale = Vector3.one * NowSize;
    //    NowT.transform.localScale = Vector3.one * NowSize;

    //    if (Input.GetKeyDown("q"))
    //    {
    //        DragObj.SpObj(Picemanager.triggergameobjecs[0].gameObject, transform);
    //    }
    //}
    //改變遊戲階段
    public void SetMod(int n)
    {
        this.n = n;
        NowT = DragObj.SpObj(Picemanager.triggergameobjecs[n + 1].gameObject, transform).GetComponent<triggergameobjec>();

        Pice_x.transform.localScale = Vector3.one * NowSize;
        NowT.transform.localScale = Vector3.one * NowSize;

        NowT.Open(NowKey);
        NowT.EnterEvent += () =>
        {
            var _T = NowT;
            LoadPice(_T, this.n + 1);
            if (this.n + 2 < nMods.Length)
            {
                SetMod(this.n + 1);
                _T.Open(NowKey);
            }
            else
            {
                NowT = null;
                _T.Close();
                this.DragObj.WinObj.SetActive(true);
                //Pice_x.gameObject.SetActive(false);
                print("勝利");
            }
        };
    }
    //把某個碎片納入主體
    public void LoadPice (triggergameobjec Obj, int Index)
    {
        Destroy(Obj.Rigidbody2d);
        Obj.transform.parent = Pice_x.AllObj[Index].obj.transform;
        Obj.transform.localPosition = Vector3.zero;
        Obj.transform.localRotation = Quaternion.identity;
        Obj.transform.localScale = Vector3.one;
    }
}
