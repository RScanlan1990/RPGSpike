using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFactory
{
    public List<Enemy> Enemies;

    public EnemyFactory(List<Enemy> enemies)
    {
        Enemies = enemies;
    }

    public Enemy CreateEnemy(int enemyId)
    {
        return Enemies.Where(e => e.Id == enemyId).FirstOrDefault();
    }
}
