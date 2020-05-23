//// Отвечает за поведение коллайдера атаки и передачу информации о нужной коллизии (передает чарактер атакуемого объекта в метод атаки)

using UnityEngine;
using System.Collections;
using Asset.Scripts.Qbik.Static.Log;

public class AttackCollision: MonoBehaviour
{
    private AttackData _attackData;

    public void Initialized(AttackData _attackData) 
    {
        this._attackData = _attackData;
    }

    private void OnEnable()
    {
        StartCoroutine(AttackCollider()); //При активации пускаем карутину отключения
    }

    IEnumerator AttackCollider()
    {
        yield return new WaitForSeconds(_attackData._timeStopAttack); //Время пока фиксировано, тут надо задавать длительность существования коллайдера
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) //Событие коллизии
    {
          if (collision.gameObject.tag == _attackData._tagAttack) //Если таг объекта нам подходит
                collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(_attackData, 0); //Передает чарактер в метода атаки родительского чарактера
    }

    private void OnDisable()
    {
        StopAllCoroutines(); //Стопим корутин при выключении объекта
    }
}