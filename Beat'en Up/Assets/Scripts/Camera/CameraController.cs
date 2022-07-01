using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Charecters;
public class CameraController : MonoBehaviour
{
    private PlayerController _player;

    private Vector3 _offset = new Vector3(0, 2.5f, 4.5f);

    private static bool _shake = false;
    public static bool ShouldShake { get { return _shake; } set { _shake = value; } }

    private float _power = 0.3f;
    private float _duration = 0.2f;
    private float _slowDownAmount = 1;
    private float _initialDuration;
    private Vector3 startPosition;

    public void Init()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _initialDuration = _duration;
    }

    private void LateUpdate()
    {
        if (_shake)
            return;
        Vector3 targetPos = new Vector3(_player.transform.position.x, 0, 0);

        targetPos = targetPos + _offset;
        transform.position = targetPos;
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        CameraShake();
    }

    private void CameraShake()
    {
        if (_player == null)
            Init();
        if (_shake)
        {
            if (_duration > 0)
            {
                transform.localPosition = startPosition + Random.insideUnitSphere * _power;
                _duration -= Time.deltaTime * _slowDownAmount;
            }
            else
            {
                _shake = false;
                _duration = _initialDuration;
                transform.localPosition = startPosition;
            }
        }
    }
}
