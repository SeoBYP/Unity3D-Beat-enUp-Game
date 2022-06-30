using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateListener : MonoBehaviour,IUpdate
{
    public enum EDIR { UP,DOWN,RIGHT,LEFT,COUNT}
    public EDIR _eDIR = EDIR.UP;
    public float _speed = 3f;
    float _targetDist;
    Vector3 _startPos;

    private void Start()
    {
        _eDIR = (EDIR)Random.Range((int)EDIR.UP, (int)EDIR.COUNT);
        _startPos = transform.position;
        _targetDist = Random.Range(1, 3);
    }

    public void OnUpdate()
    {
        float dist = Vector3.Magnitude(transform.position - _startPos);

        if (_targetDist * _targetDist < dist)
            return;

        switch (_eDIR)
        {
            case EDIR.UP:
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
                break;
            case EDIR.DOWN:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                break;
            case EDIR.RIGHT:
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
                break;
            case EDIR.LEFT:
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
                break;
        }
    }

    public void OnKnockBack()
    {
        //throw new System.NotImplementedException();
    }
}
