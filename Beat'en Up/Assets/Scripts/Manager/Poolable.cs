using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    public bool IsUsing;
    private int timer = 1;

    public void Active()
    {
        IsUsing = true;
        this.gameObject.SetActive(true);
        StartCoroutine(Deactive());
    }

    IEnumerator Deactive()
    {
        yield return new WaitForSeconds(timer);
        IsUsing = false;
        this.gameObject.SetActive(false);
    }
}
