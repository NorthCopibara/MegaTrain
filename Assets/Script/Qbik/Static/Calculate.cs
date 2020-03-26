
namespace Asset.Scripts.Qbik.Static.Calculate
{
    public static class Calculate 
    {
        public static PlayerData CalculatePlayer() 
        {
            PlayerData data = new PlayerData(100, 10, 5, 1, 25); //Сюда пушим статик данные из json
            int lvl = 1; //Сюда приходит из json сохраненные данные о уровне

            data._health = data._health + lvl * 2;
            data._damage = data._damage + lvl * 2;

            return data;
        }
    }
}
