using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutineManager : MonoBehaviour
{
    int myTest;

    private void Start()
    {
        CoroutinesManager.myCoroutinesManager.MyStartCoroutine(Test());
    }
    private IEnumerator Test()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Test " + myTest);
        myTest++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            CoroutinesManager.myCoroutinesManager.MyStartCoroutine(Test());
        }
    }
}
