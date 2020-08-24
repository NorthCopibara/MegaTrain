using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using JokerGho5t.MessageSystem;
using Qbik.Game.ZoneGame.StageGame;

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

        FlySpawnController flySpawnController;
        MapController mapController;
        PlayerManager playerManager;
        AnimTP animTP;

        private void Start()
        {
            Message.AddListener<ControlSystemMessage>("InitControlSystem", Init);

            Message.Send("InitDataLevel");
            Message.Send("SpawnGolem");
        }

        public void Init(ControlSystemMessage controlSystemMessage)
        {
            Message.RemoveListener<ControlSystemMessage>("InitControlSystem", Init);

            flySpawnController = controlSystemMessage._flySpawnController;
            mapController = controlSystemMessage._mapController;
            playerManager = controlSystemMessage._playerManager;
            animTP = controlSystemMessage._animTP;
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
            flySpawnController?.Destroy();
            mapController?.Destroy();
            playerManager?.Destroy();
            animTP?.Destroy();

            Model.ClearLvl();
        }
    }

    public class ControlSystemMessage : Message
    {
        private string nameEvent;
        private PlayerManager playerManager;
        private FlySpawnController flySpawnController;
        private MapController mapController;
        private AnimTP animTP;
        public PlayerManager _playerManager => playerManager;
        public FlySpawnController _flySpawnController => flySpawnController;
        public MapController _mapController => mapController;
        public AnimTP _animTP => animTP;

        public ControlSystemMessage(string nameEvent, PlayerManager playerManager, FlySpawnController flySpawnController, MapController mapController, AnimTP animTP)
        {
            this.nameEvent = nameEvent;
            this.playerManager = playerManager;
            this.flySpawnController = flySpawnController;
            this.mapController = mapController;
            this.animTP = animTP;
        }

        public static void SendEvent(string gameEvent, PlayerManager playerManager, FlySpawnController flySpawnController, MapController mapController, AnimTP animTP)
        {
            SendEvent(new ControlSystemMessage(gameEvent, playerManager, flySpawnController, mapController, animTP));
        }

        private static void SendEvent(ControlSystemMessage gameEventMessage)
        {
            Send(gameEventMessage.nameEvent, gameEventMessage);
        }
    }
}
