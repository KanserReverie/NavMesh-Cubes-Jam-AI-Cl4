using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Steering
{
    [CreateAssetMenu(menuName = "Steering/Composite", fileName = "Composite", order = -100)]
    public class CompositeBehaviour : SteeringBehaviour
    {
        [Serializable]
        public struct WeightedBehaviour
        {
            [Min(0.1f)]
            public float weighting;
            public SteeringBehaviour behaviour;
        }

        [SerializeField] public List<WeightedBehaviour> behaviours = new List<WeightedBehaviour>();

        public override Vector3 Calculate(SteeringAgent _agent)
        {
            Vector3 force = _agent.CurrentForce;

            behaviours.ForEach(weighted =>
            {
                force += weighted.behaviour.Calculate(_agent) * weighted.weighting;
            });
            
            return force;
        }

    }
}