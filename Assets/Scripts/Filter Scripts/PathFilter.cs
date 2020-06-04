using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Path")]
public class PathFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        foreach(Transform item in original)
        {
            Path path = item.GetComponentInParent<Path>();
            if(path != null)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
