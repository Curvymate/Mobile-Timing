using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : SObject
{
    private static List<Hook> activeHooks = new List<Hook>();
    public static Hook GetClosestHook_Horizontal(Vector2 position)
    {
        Hook ret;

        float dist = Mathf.Infinity;
        float dx = 0;

        int index = 0;

        for (int i = 0; i < activeHooks.Count; i++)
        {
            dx = Mathf.Abs(activeHooks[i].Position.x - position.x);

            if (dx < dist)
            {
                dist = dx;
                index = i;
            }
        }

        ret = activeHooks[index];

        return ret;
    }

    public override void Init(Movement target)
    {
        base.Init(target);
        activeHooks.Add(this);
    }

    private void OnDestroy()
    {
        activeHooks.Remove(this);
    }
}