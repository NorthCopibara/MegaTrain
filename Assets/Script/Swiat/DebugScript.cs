using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    public InitTunnelData data;
	bool x = false;
	bool y = false;

    void Update()
    {
        if (data.isTunnel() && data._endIntervals && !x)
        {
            Debug.Log("Появился");
			x = true;
        }
		else if(data._isEndAll && !y && x)
		{
			Debug.Log("Закончился");
			y = true;
		}
    }
}
