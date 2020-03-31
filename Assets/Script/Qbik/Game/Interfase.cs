public interface ICanAttack 
{
    void Attack();
}

public interface ITakeDamage 
{
    void TakeDamage(AttackData attack);
}

public interface ICam 
{
    void SwapCam(int i, int j);
}