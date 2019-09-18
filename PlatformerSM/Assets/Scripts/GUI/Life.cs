using Assets.Scripts.SceneConfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField]
    private int maxHp;
    private int currentHP;
    [SerializeField]
    private GameObject stat;

    private List<GameObject> stats;
    public int CurrentHP { get => currentHP;
        set
        {
            if (value > currentHP && value <= maxHp)
            {
                for (int i = currentHP; i < value;++i)
                {
                    Vector3 newPosition = new Vector3(transform.position.x + 1.8f * currentHP, transform.position.y, transform.position.z);
                    stats.Add(Instantiate(stat, transform, false));
                    stats[stats.Count - 1].transform.position = newPosition;
                    currentHP++;
                }
                
            }
            if (value < currentHP && value >= 0)
            {
                for (int i = value; i <= currentHP; ++i)
                {
                    Destroy(stats[stats.Count - 1]);
                    stats.RemoveAt(stats.Count - 1);
                    currentHP--;
                }

            }
            if(value == 0)
            {
                SceneConfig.KillPlayer();
            }
            
        }
    }

    void Start()
    {
        currentHP = maxHp;
        stats = new List<GameObject>();
        for (int i = 0; i < currentHP; ++i)
        {
            Vector3 newPosition = new Vector3(transform.position.x+ 1.8f * i, transform.position.y, transform.position.z);
            stats.Add(Instantiate(stat, transform, false));
            stats[i].transform.position = newPosition;
        }


    }

    
    
}
