using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int StatIncreaseByFloorIntervals = 3;
    private Vector3 _position;
    private static float[] Mod =    {0.05f,  0, 0.001f, 0.025f, 0, 0};

    // [Hp, Def, Speed, Atk, Vision, Detect]

    private static class MediumModifier
    {
        public static float[] Green =  {6f, 0.2f, 0.5f, 0.2f, 0, 0};

        public static float[] Blue =   {8f, 0.1f, 0.2f, 0.4f, 0, 0};
        
        public static float[] Purple = {8f, 0.1f, 0.2f, 0.4f, 0, 0};
        
        public static float[] Red =    {25f, 0.1f, 0.5f, 1f, 0, 0};
    }
    
    private static class HardModifier
    {
        public static float[] Green =  {14f, 0.25f, 0.9f, 0.5f, 0, 0};
        
        public static float[] Blue =   {16f, 0.15f, 0.5f, 1f, 0, 0};
        
        public static float[] Purple = {16f, 0.15f, 0.5f, 1f, 0, 0};
        
        public static float[] Red =    {50f,  0.2f,   1f, 3f, 0, 0};
    }

    private void Start()
    {
        _position = transform.position;
        _position.z = -1;
    }

    public GameObject SpawnEnemy(EnemyTypes.EnemyType enemy, MineManager manager, int difficulty, int floor)
    {
        GameObject spawnObject;
        BaseEnemy spawnEnemy;
        
        float[] diffMod;
        float[] floorMod;
        switch (difficulty)
        {
            case 1:
                diffMod = floorMod = GetMediumModifier(enemy);
                break;
            case 2:
                diffMod = floorMod = GetHardModifier(enemy);
                break;
            default:
                diffMod = new[] {0f, 0f, 0f, 0f, 0f, 0f};
                floorMod = new[] {1f, 1f, 1f, 1f, 1f, 1f};
                break;
        }

        switch (enemy)
        {
            case EnemyTypes.EnemyType.Green:
                spawnObject = Instantiate(manager.GreenEnemy, _position, transform.rotation);
                break;
            case EnemyTypes.EnemyType.Blue:
                spawnObject = Instantiate(manager.BlueEnemy, _position, transform.rotation);
                break;
            case EnemyTypes.EnemyType.Purple:
                spawnObject = Instantiate(manager.PurpleEnemy, _position, transform.rotation);
                break;
            case EnemyTypes.EnemyType.Red:
                spawnObject = Instantiate(manager.RedEnemy, _position, transform.rotation);
                break;
            default:
                spawnObject = Instantiate(manager.GreenEnemy, _position, transform.rotation);
                break;
        }

        spawnEnemy = spawnObject.GetComponent<BaseEnemy>();

        // [Hp, Def, Speed, Atk, Vision, Detect]
        spawnEnemy.health += diffMod[0] + Mod[0] * floorMod[0] * floor;
        spawnEnemy.defense += diffMod[1] + Mod[1] * floorMod[1] * floor;
        spawnEnemy.speed += diffMod[2] + Mod[2] * floorMod[2] * floor;
        spawnEnemy.attack += diffMod[3] + Mod[3] * floorMod[3] * floor;
        spawnEnemy.vision += diffMod[4] + Mod[4] * floorMod[4] * floor;
        spawnEnemy.detect += diffMod[5] + Mod[5] * floorMod[5] * floor;
        
        return spawnObject;
    }

    private float[] GetMediumModifier(EnemyTypes.EnemyType enemy)
    {
        switch (enemy)
        {
            case EnemyTypes.EnemyType.Green:
                return MediumModifier.Green;
            case EnemyTypes.EnemyType.Blue:
                return MediumModifier.Blue;
            case EnemyTypes.EnemyType.Purple:
                return MediumModifier.Purple;
            case EnemyTypes.EnemyType.Red:
                return MediumModifier.Red;
            default:
                return MediumModifier.Green;
        }
    }

    private float[] GetHardModifier(EnemyTypes.EnemyType enemy)
    {
        switch (enemy)
        {
            case EnemyTypes.EnemyType.Green:
                return HardModifier.Green;
            case EnemyTypes.EnemyType.Blue:
                return HardModifier.Blue;
            case EnemyTypes.EnemyType.Purple:
                return HardModifier.Purple;
            case EnemyTypes.EnemyType.Red:
                return HardModifier.Red;
            default:
                return HardModifier.Green;
        }
    }


}
