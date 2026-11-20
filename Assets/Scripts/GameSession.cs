// Ќовый скрипт. ’ранит счЄт вишен между уровн€ми (статический класс Ч на сцену вешать не нужно).
public static class GameSession
{
    public static int TotalCherries { get; private set; }

    // «начение на момент начала текущего уровн€ Ч нужно, чтобы при рестарте вишни не дублировались
    private static int cherriesAtLevelStart;

    public static void Reset()
    {
        TotalCherries = 0;
        cherriesAtLevelStart = 0;
    }

    public static void BeginLevel()
    {
        cherriesAtLevelStart = TotalCherries;
    }

    public static void RollbackLevel()
    {
        TotalCherries = cherriesAtLevelStart;
    }

    public static void AddCherry()
    {
        TotalCherries++;
    }
}