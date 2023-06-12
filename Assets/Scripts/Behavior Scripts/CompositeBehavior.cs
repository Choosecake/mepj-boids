using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    [System.Serializable]
    public class SetBehavior
    {
        public FlockBehavior Behaviour;
        public float Weight = 1;
    }

    public SetBehavior[] behaviours;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 move = Vector2.zero;

        foreach (var behavior in behaviours)
        {
            Vector2 partialMove = behavior.Behaviour.CalculateMove(agent, context, flock) * behavior.Weight;

            if (partialMove.magnitude == 0) continue;

            if (partialMove.sqrMagnitude > behavior.Weight * behavior.Weight)
            {
                partialMove.Normalize();
                partialMove *= behavior.Weight;
            }

            move += partialMove;
        }

        return move;
    }
}