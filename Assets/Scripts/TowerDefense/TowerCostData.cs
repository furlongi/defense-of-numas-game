namespace TowerDefense
{
    public static class TowerCostData
    {

        public static int MAXTier = 3;
        
        public static int[][] LightTower =
        {
            new int[] {17, 3, 0, 0},    // Initial purchase
            new int[] {15, 7, 3, 0},    // Tier 1
            new int[] {10, 10, 9, 3},  // Tier 2
            new int[] {10, 10, 10, 10},  // Tier 3
        };

        public static int[][] MediumTower =
        {
            new int[] {15, 2, 0, 0},    // Initial purchase
            new int[] {11, 6, 3, 0},    // Tier 1
            new int[] {8, 8, 7, 2},     // Tier 2
            new int[] {9, 9, 9, 9},     // Tier 3
        };
        
        public static int[][] HeavyTower =
        {
            new int[] {14, 2, 0, 0},    // Initial purchase
            new int[] {11, 6, 3, 0},    // Tier 1
            new int[] {8, 8, 7, 2},  // Tier 2
            new int[] {9, 9, 9, 9},  // Tier 3
        };

    }
}