using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitTunnelData : MonoBehaviour
{
    public int[] spawnTunnelFirstIntervalInMinuts = new int[2];
    public int[] spawnTunnelSecondIntervalInMinuts = new int[2];

    private bool _isTunnelStart = false;
    private bool _isTunnelEnd = false;
    private bool _isFirstInterval = true; //интервал до участка спавна
    private bool _isSecondInterval = false; //интервал внутри участка спавна
    public bool _endIntervals = false;
    public bool _isEndAll = false;
    private int _lengthTunnelInterval;
    private int _lengthFirstInterval;
    private int _lengthSecondInterval;
    private float _timeLeft = 0;

    void Start()
    {
        InitInterval();
        _lengthTunnelInterval = (int)Random.Range(1.0f, 4.0f) * 60;
        _lengthFirstInterval = (int)Random.Range(spawnTunnelFirstIntervalInMinuts[0] * 60, spawnTunnelFirstIntervalInMinuts[1] * 60);
        _lengthSecondInterval = (int)Random.Range(spawnTunnelSecondIntervalInMinuts[0] * 60, spawnTunnelSecondIntervalInMinuts[1] * 60);
    }

    void Update()
    {
        //пока не закончились все действия с туннелем
        if (!_isEndAll)
        {
            //пока первый интервал
            if (_isFirstInterval && !_endIntervals)
            {
                if (_timeLeft  < _lengthFirstInterval)
                {
                    _timeLeft += Time.deltaTime;
                }
                else
                {
                    _isFirstInterval = false;
                    _isSecondInterval = true;
                    _timeLeft = 0;
                }
            }
            //пока второй интервал
            else if (_isSecondInterval && !_endIntervals)
            {
                /*!!!!!
                 тут спавнятся предупредительные таблички о скором приближении туннеля
                 !!!!!*/
                if (_timeLeft < _lengthSecondInterval)
                {
                    _timeLeft += Time.deltaTime;
                }
                else
                {
                    _isTunnelStart = true;
                    _isSecondInterval = false;
                    _endIntervals = true;
                    _timeLeft = 0;
                }
            }
            //интервалы закончились и появился туннель
            else if (_endIntervals)
            {
                //спавн туннеля

                if (_timeLeft < _lengthTunnelInterval)
                {
                    _timeLeft += Time.deltaTime;
                }
                else
                {
                    _isTunnelStart = false;
                    _isEndAll = true;
                    _isTunnelEnd = true;
                }
            }
        }
    }

    private void InitInterval()
    {
        spawnTunnelFirstIntervalInMinuts[0] = 1;
        spawnTunnelFirstIntervalInMinuts[1] = 7;

        spawnTunnelSecondIntervalInMinuts[0] = 2;
        spawnTunnelSecondIntervalInMinuts[1] = 4;
    }

    public bool isTunnel()
    {
        if (_isTunnelStart)
        {
            return true;
        }
        else if (_isTunnelEnd)
        {
            return false;
        }
        else
        {
            return false;
        }
    }
}