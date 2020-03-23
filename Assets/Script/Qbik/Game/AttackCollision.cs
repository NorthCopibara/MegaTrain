//// Отвечает за поведение коллайдера атаки и передачу информации о нужной коллизии (передает чарактер атакуемого объекта в метод атаки)

using UnityEngine;
using System.Collections;
using Asset.Scripts.Qbik.Statick.Log;

public class AttackCollision: MonoBehaviour
{
    [SerializeField] private string TagAttack; //Прописывается цель атаки
    [SerializeField] private GameObject _characterObj; //Грубая зацепка за родительский объект, сделал так что бы не создавалась путаница, что к кому относится 
    ICanAttack iAttack; //Интерфейс атаки родительского класса

    private void Awake()
    {
        iAttack = _characterObj.GetComponent<ICanAttack>();
    }

    private void OnEnable()
    {
        StartCoroutine(AttackCollider()); //При активации пускаем карутину отключения
    }

    IEnumerator AttackCollider()
    {
        yield return new WaitForSeconds(0.3f); //Время пока фиксировано, тут надо задавать длительность существования коллайдера
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) //Событие коллизии
    {
        /*if (collision.gameObject.tag == TagAttack) //Если таг объекта нам подходит
            iAttack.Attack(collision.gameObject.GetComponent<Character>()); //Передает чарактер в метода атаки родительского чарактера*/
    }

    private void OnDisable()
    {
        StopAllCoroutines(); //Стопим корутин при выключении объекта
    }
}
