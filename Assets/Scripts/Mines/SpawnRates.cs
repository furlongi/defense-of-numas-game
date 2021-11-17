
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class SpawnRates
{

    public const float RateIncreaseByFloor = 0.005f;
    // 10 floors * .005 = 0.05 rate increase
    
    public const float RateIncreaseByDifficulty = 0.0025f;
    // Additional 0.0025 if Medium and 0.005 if Hard

    public const float MaxIncrease = 0.13f;

    public const float SpawnIncreaseByFloor = 0.05f;
    public const float SpawnIncreaseByDifficulty = 0.5f;
    

    public static class Easy
    {
        public const float GreenRate = 0.75f;
        public const float BlueRate = 0.20f;
        public const float PurpleRate = 0.09f;
        public const float RedRate = 0.01f;
    }
    
    public static class Medium
    {
        public const float GreenRate = 0.50f;
        public const float BlueRate = 0.25f;
        public const float PurpleRate = 0.18f;
        public const float RedRate = 0.07f;
    }
    
    public static class Hard
    {
        public const float GreenRate = 0.35f;
        public const float BlueRate = 0.30f;
        public const float PurpleRate = 0.25f;
        public const float RedRate = 0.10f;
    }
    

    public static List<EnemyType> CalculateProbability(int difficulty, int floor, int spawns)
    {
        float[] dist = GetList(difficulty);
        int toSpawn = GetFloorSpawnNumber(difficulty, floor, spawns);
        return CreateEnemyList(dist, toSpawn, difficulty, floor);
    }

    private static float[] GetList(int diff)
    {
        switch (diff)
        {
            case 0:
                return new[] {Easy.GreenRate, Easy.BlueRate, Easy.PurpleRate, Easy.RedRate};
            case 1:
                return new[] {Medium.GreenRate, Medium.BlueRate, Medium.PurpleRate, Medium.RedRate};
            case 2:
                return new[] {Hard.GreenRate, Hard.BlueRate, Hard.PurpleRate, Hard.RedRate};
            default:
                return new[] {Easy.GreenRate, Easy.BlueRate, Easy.PurpleRate, Easy.RedRate};
        }
    }

    private static List<EnemyType> CreateEnemyList(float[] dist, int toSpawn, int diff, int floor)
    {
        List<EnemyType> result = new List<EnemyType>();
        for (int i = 0; i < toSpawn; i++)
        {
            float roll = GetEnemyRoll(diff, floor);
            result.Add(SolveProbability(dist, roll));
        }

        return result;
    }

    private static int GetFloorSpawnNumber(int difficulty, int floor, int spawns)
    {
        return Math.Min(spawns, (int)Math.Floor(Random.Range(1f, spawns) + SpawnIncreaseByDifficulty * floor + SpawnIncreaseByFloor * difficulty));
    }

    private static float GetEnemyRoll(int difficulty, int floor)
    {
        return Random.Range(0f, 1f) + Math.Min(MaxIncrease, RateIncreaseByDifficulty * difficulty + RateIncreaseByFloor * floor);
    }

    private static EnemyType SolveProbability(float[] dist, float roll)
    {
        for (int i = 0; i < 4; i++)
        {
            if (roll <= dist[i])
            {
                return EnemyList[i];
            }
        }

        return EnemyType.Red;
    }
    
    public enum EnemyType
    {
        Green,
        Blue,
        Purple,
        Red
    }
    
    private static EnemyType[] EnemyList = {EnemyType.Green, EnemyType.Blue, EnemyType.Purple, EnemyType.Red};

}
