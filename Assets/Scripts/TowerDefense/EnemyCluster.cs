using System.Runtime.ConstrainedExecution;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyCluster
    {
        public static int GREEN = 1;
        public static int BLUE = 2;
        public static int RED = 3;
        
        public int EnemyID { get; set; }
        public int Count { get; set; }
        
        public EnemyCluster()
        {
            EnemyID = 1;
            Count = 0;
        }

        public EnemyCluster(int enemyId, int count)
        {
            EnemyID = enemyId;
            Count = count;
        }
    }
}