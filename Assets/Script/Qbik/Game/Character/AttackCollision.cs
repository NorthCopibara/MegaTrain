using UnityEngine;
using System.Collections;

namespace Qbik.Game
{
    public class AttackCollision : MonoBehaviour
    {
        private AttackData _attackData;

        public void Initialized(AttackData _attackData)
        {
            this._attackData = _attackData;
        }

        private void OnEnable()
        {
            StartCoroutine(AttackCollider()); 
        }

        IEnumerator AttackCollider()
        {
            yield return new WaitForSeconds(_attackData._timeStopAttack); 
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision) 
        {
            if (collision.gameObject.tag == _attackData._tagAttack) 
                collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(_attackData, 0);
        }

        private void OnDisable()
        {
            StopAllCoroutines(); 
        }
    }
}