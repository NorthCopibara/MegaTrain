using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asset.Scripts.Qbik.Statick.Log
{
    public static class Log
    {
        private static string nameCheck;

        public static void MyDebugLog(string script, string metod, string value)
        {
            if(script == nameCheck)
                Debug.Log(script + "/" + metod + ": " + value + "\n");
        }

        public static void SetupLog(string name) 
        {
            nameCheck = name;
        }
    }
}
