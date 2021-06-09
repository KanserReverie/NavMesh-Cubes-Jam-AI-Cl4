using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering.Extras
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SteeringAgent prefab;
        [SerializeField] private float spawnRadius = 10;
        [SerializeField] private int spawnCount = 10;


        // Start is called before the first frame update
        private void Awake()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
                SteeringAgent boid = Instantiate(prefab);
                boid.transform.position = pos;
                boid.transform.forward = Random.insideUnitSphere.normalized;

                boid.SetColor(Random.ColorHSV(0, 1, 1, 1, 1, 1));
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, spawnRadius);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}