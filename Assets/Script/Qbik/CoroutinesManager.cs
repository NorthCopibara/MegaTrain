using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinesManager : MonoBehaviour
{
    //Плохая реализация. Нужна очередь!
    //Пока сделан только для одного локального случая

    private static CoroutinesManager coroutinesManager;
    public static CoroutinesManager myCoroutinesManager => coroutinesManager;

    private void OnEnable()
    {
        coroutinesManager = this;
    }

    public void MyStartCoroutine(IEnumerator coroutine) 
    {
        StartCoroutine(coroutine);
    }

    public void MyStopAllCoroutines() 
    {
        StopAllCoroutines();
    }
}
