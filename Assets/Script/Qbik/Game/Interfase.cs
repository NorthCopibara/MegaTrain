public interface ICanAttack 
{
    void Attack();
}

public interface ITakeDamage 
{
    void TakeDamage(AttackData attack, int num);
}

public interface ICam 
{
    void SwapCam(int i, int j);
}

public interface IPlayer 
{
    void Init();
}