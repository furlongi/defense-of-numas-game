
namespace TowerDefense
{
    public class EnemyCluster
    {
        public static int GREEN = 1;
        public static int BLUE = 2;
        public static int PURPLE = 3;
        public static int RED = 4;

        public int EnemyID;
        public int Count;
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