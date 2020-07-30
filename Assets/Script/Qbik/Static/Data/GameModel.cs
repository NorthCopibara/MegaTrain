using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Static.Data
{
    public class GameModel : MonoBehaviour
    {
        private int numberCar;
        private int numberSpawn;

        private GameObject endGame;
        private Zone dataZone;
        private LvlState lvl;
        private ControlSystemData csData;
        private State stateGame;

        public GameObject EndGame => endGame;

        public Zone DataZone => dataZone;

        public int NumberCar => numberCar;

        public int NumberSpawn => numberSpawn;

        public LvlState Lvl => lvl;

        public ControlSystemData CSData => csData;

        public State StateGame => stateGame;

        public GameModel(LvlState lvl, State stateGame, ControlSystemData csData, Zone dataZone, GameObject endGame) 
        {
            this.endGame = endGame;
            this.dataZone = dataZone;
            this.lvl = lvl;
            this.stateGame = stateGame;
            this.csData = csData;
        }

        public void UpCar()
        {
            numberCar++;
        }

        public void SetSpawn(int spawn)
        {
            numberSpawn = spawn;
        }

        public void UpSpawn()
        {
            numberSpawn++;
        }

        public void SetStateGame(State state)
        {
            stateGame = state;
        }

        public void SetStateLvl(LvlState state)
        {
            lvl = state;
        }

        public void ClearModel() 
        {
            numberCar = 0;
            numberSpawn = 0;
        }
    }
}
