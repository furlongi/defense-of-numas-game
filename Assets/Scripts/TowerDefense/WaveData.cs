using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveData : MonoBehaviour
{
    // Start is called before the first frame update
    private EventManager _eventManager;
    
    [System.Serializable]
    public class RoundData
    {
        [System.Serializable]
        public class EnemyCluster
        {
            public EnemyTypes.EnemyType enemyType = EnemyTypes.EnemyType.Green;
            public int number = 1;
        }

        public List<EnemyCluster> roundData = new List<EnemyCluster>();
    }

    public List<RoundData> wave1 = new List<RoundData>();
    public List<RoundData> wave2 = new List<RoundData>();
    public List<RoundData> wave3 = new List<RoundData>();

    void Awake()
    {
        _eventManager = GetComponentInParent<EventManager>();
    }
}
