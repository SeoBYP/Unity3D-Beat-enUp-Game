using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveObject : MonoBehaviour
{
    public void Deactive()
    {
        this.gameObject.SetActive(false);
    }
}
