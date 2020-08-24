using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using JokerGho5t.MessageSystem;

namespace Qbik.Game
{
    public class ControlSystem : MonoBehaviour
    {
        #region Deligate
        public delegate void UpdateController();
        public static event UpdateController update;

        public delegate void FixedUpdateController();
        public static event FixedUpdateController fixedUpdate;
        #endregion
        
        private void Start()
        {
            Message.Send("InitDataLevel");
            Message.Send("SpawnGolem");
        }

        #region Update
        private void Update()
        {
            update?.Invoke();
        }

        private void FixedUpdate()
        {
            fixedUpdate?.Invoke();
        }
        #endregion

        private void OnDestroy()
        {
            Model.ClearLvl();
        }
    }
}
