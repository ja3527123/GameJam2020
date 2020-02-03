using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggergameobjec : MonoBehaviour
{
    public Collider2D[] Triggers;

    public Rigidbody2D Rigidbody2d;

    public string NowKey => Mainmanager._.NowKey;

    public Action EnterEvent;

    public void Open (string Key)
    {
        bool b = false;
        for (int i = 0; i < Triggers.Length; i++)
        {

            if (Triggers[i].name == Key)
            {
                Triggers[i].gameObject.SetActive(true);
                b = true;
            }
            else
            {
                Triggers[i].gameObject.SetActive(false);
            }
        }
        if (b)
        {
            gameObject.SetActive(true);
        }
    }
    public void Close ()
    {
        foreach (var item in Triggers)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (NowKey == collision.name)
        {
            EnterEvent?.Invoke();
        }
    }
}
