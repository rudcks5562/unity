using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Prefabs
    public Transform[] ObjectSpawnPoints;
    public GameObject Pot;
    public GameObject Valve;

    // member variables
    public int m_PotCount = 3;
    public int m_valveCount = 3;
    int m_SpawnPointCount;

    void Awake()
    {
        GenerateRandomObjectSpawnPoints();
    }

    void GenerateRandomObjectSpawnPoints()
    {
        m_SpawnPointCount = ObjectSpawnPoints.Length;
        int count = 0;

        int[] seq = new int[m_SpawnPointCount];
        bool[] isSelected = new bool[m_SpawnPointCount];
        
        // 랜덤한 수열 생성
        while (count < m_PotCount + m_valveCount) {
            int number = Random.Range(0, m_SpawnPointCount);
            if (isSelected[number]) continue;
            isSelected[number] = true;
            seq[count++] = number;
        }

        for (int i = 0; i < m_PotCount + m_valveCount; i++) {
            if (i < 3) {
                // 솥 프리팹 배치
                Instantiate(Pot, ObjectSpawnPoints[seq[i]].transform.position, Quaternion.identity);
            } else {
                Instantiate(Valve, ObjectSpawnPoints[seq[i]].transform.position, Quaternion.identity);
            }
        }
    }
}
