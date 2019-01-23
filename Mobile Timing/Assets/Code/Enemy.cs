using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SObject, IColor
{
    public static List<Enemy> activeEnemies = new List<Enemy>();
    public static Enemy ClosestEnemy(Vector2 position)
    {
        Enemy ret = null;

        float mag;
        float minMag = Mathf.Infinity;

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            mag = (activeEnemies[i].position - position).sqrMagnitude;

            if (mag < minMag)
            {
                ret = activeEnemies[i];
                minMag = mag;
            }
        }

        return ret;
    }

    public Color color { get { return _color; } }
    private Color _color;

    private new void Awake()
    {
        activeEnemies.Add(this);
    }

    public override void Init(Movement target, IColor targetColorProfile)
    {
        base.Init(target, targetColorProfile);

        _color = Utility.GetRandomColor();
    }
}
