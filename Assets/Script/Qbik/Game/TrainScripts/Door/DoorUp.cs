using UnityEngine;

using Qbik.Static.Data;
using Qbik.Game.EnemyGame;
using Qbik.Game.Data;

namespace Qbik.Game.ZoneGame.LogicZone
{
    public class DoorUp : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        [SerializeField] private GameObject _train;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (AllData.Lvl == LvlState.Last)
                return;

            if (collision.tag == "Player")
            {
                _button.SetActive(true);
                _button.GetComponent<EventData>().SetButton(collision.gameObject, _train, 3);
            }

            if (collision.tag == "Enemy")
            {
                EnemyAI ai = collision.GetComponent<EnemyAI>();
                if (ai.CheckState())
                {
                    ai.InitNewState(State.Roof);
                    collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 3 * 2, 0);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _button.SetActive(false);
            }
        }
    }
}
