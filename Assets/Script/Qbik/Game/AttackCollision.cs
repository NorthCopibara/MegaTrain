//// Отвечает за поведение коллайдера атаки и передачу информации о нужной коллизии (передает чарактер атакуемого объекта в метод атаки)

using UnityEngine;
using System.Collections;
using Asset.Scripts.Qbik.Statick.Log;

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
        yield return new WaitForSeconds(0.3f); //Время пока фиксировано, тут надо задавать длительность существования коллайдера
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) //Событие коллизии
    {
        if (collision.gameObject.tag == _attackData._targetAttack) //Если таг объекта нам подходит
            collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(_attackData); //Передает чарактер в метода атаки родительского чарактера
    }

    private void OnDisable()
    {
        StopAllCoroutines(); //Стопим корутин при выключении объекта
    }
}

public enum TypeAttack 
{
    Fiz,
    Mag
}

public struct AttackData
{
    public int _damage;
    public string _targetAttack;
    public TypeAttack _type;

    public AttackData(int _damage, string _targetAttack, TypeAttack _type) 
    {
        this._damage = _damage;
        this._targetAttack = _targetAttack;
        this._type = _type;
    }
}
