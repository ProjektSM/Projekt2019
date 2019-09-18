using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneConfig
{
    static class SceneConfig
    {
        public static void KillPlayer()
        {
          
            GameObject.FindObjectOfType<TimeMenager>().DisableSlowmotion = true;
            Player[] players = GameObject.FindObjectsOfType<Player>();
            foreach (Player player in players)
            {
                
                if (player.isLocal)
                {
                    GameObject.Find("MainAudio").GetComponent<MainAudio>().DyingSound();
                    player.Life.CurrentHP = player.MaxLife;
                    player.transform.position = player.SpawnPosition;
                }
                    
            }
        }
        public static void ResetLevelALL()
        {

            GameObject.FindObjectOfType<TimeMenager>().DisableSlowmotion = true;
            Player[] players = GameObject.FindObjectsOfType<Player>();
            
            foreach (Player player in players)
            {
                if (player.isLocal)
                {
                    player.Life.CurrentHP = player.MaxLife;
                    player.SpawnPosition = player.BeginPosition;
                    player.transform.position = player.BeginPosition;
                }
            }
            Enemy[] enemys = GameObject.FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemys)
            {
                enemy.isAlive = true;
                enemy.transform.position = enemy.BeginPosition;
            }
        }
    }
}
