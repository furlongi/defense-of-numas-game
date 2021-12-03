namespace TowerDefense
{
    public static class TowerCostData
    {

        public static int MAXTier = 3;
        
        public static int[][] LightTower =
        {
            new int[] {0, 0, 0, 0},    // Initial purchase
            new int[] {0, 0, 0, 0},   // Tier 1
            new int[] {40, 25, 10, 5},  // Tier 2
            new int[] {50, 30, 15, 7},  // Tier 3
        };

        public static int[][] MediumTower =
        {
            new int[] {0, 0, 0, 0},    // Initial purchase
            new int[] {0, 0, 0, 0},   // Tier 1
            new int[] {0, 0, 0, 0},  // Tier 2
            new int[] {0, 0, 0, 0},  // Tier 3
        };
        
        public static int[][] HeavyTower =
        {
            new int[] {0, 0, 0, 0},    // Initial purchase
            new int[] {30, 15, 5, 0},   // Tier 1
            new int[] {40, 25, 10, 5},  // Tier 2
            new int[] {50, 30, 15, 7},  // Tier 3
        };

    }
}