using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JokerGho5t.MessageSystem;

namespace Qbik.Static.Data
{
    public class PlayerModel
    {
        private Player defoultPlayer;

        private IPlayer iPlayer;
        private PlayerData playerData;
        public PlayerData PlayerData => playerData;

        public PlayerModel(Player defoultPlayer) 
        {
            this.defoultPlayer = defoultPlayer;

            NewPlayer();
        }

        public void SetPlayer(PlayerData data)
        {
            playerData = data;
        }

        public void SetPlayerInterface(IPlayer player)
        {
            iPlayer = player;
        }

        private void NewPlayer()
        {
            playerData = new PlayerData();
            playerData._exp = 0;
            playerData._upExp = defoultPlayer._upExp;
            playerData._lvl = 1;
            playerData._health = defoultPlayer._health;
            playerData._armor = defoultPlayer._armor;
            playerData._damage = defoultPlayer._damage;
            playerData._damageForce = defoultPlayer._damageForce;
            playerData._attackRate = defoultPlayer._attackRate;
            playerData._speed = defoultPlayer._speed;
            playerData._extraJump = defoultPlayer._extraJump;
            playerData._jumpForce = defoultPlayer._jumpForce;
            playerData._attackRange = defoultPlayer._attackRange;
            playerData._nextAttackTime = defoultPlayer._nextAttackTime;
        }

        public void AddExp(int exp)
        {
            playerData._exp += exp;

            while (playerData._exp > playerData._upExp)
            {
                playerData._exp -= playerData._upExp;
                Calculate.Calculate.UpPlayer();
                iPlayer.Init();
            }

        }
    }
}
