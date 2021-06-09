using UnityEngine;

namespace Steering
{
    public class SteeringAgent : MonoBehaviour
    {
        public Vector3 Position => transform.localPosition;
        public Quaternion Rotation => transform.localRotation;

        public Vector3 Forward => transform.forward;
        public Vector3 Right => transform.right;
        public Vector3 Up => transform.up;

        public Vector3 CurrentForce => currentForce;
        // complete oppostite of "serilize field"
        [System.NonSerialized] public Vector3 velocity;

        public float Speed => speed;
        public float ViewAngle => viewAngle;
        public float MovementSmoothing => smoothing;

        [SerializeField, Range(0.01f, 0.1f)] private float smoothing = 0.05f;
        [SerializeField, Range(45f, 180f)] private float viewAngle = 180f;
        [SerializeField] private new MeshRenderer renderer;
        [SerializeField] private SteeringBehaviour behaviour;

        private Vector3 currentForce;
        private float speed;

        public void SetPosAndRot(Vector3 _pos, Quaternion _rot)
        {
            transform.localPosition = _pos;
            transform.localRotation = _rot;
        }

        public void Initalise(float _speed) => speed = _speed;
        public void UpdateAgent() => behaviour?.UpdateAgent(this);
        public void UpdateCurrentForce(Vector3 _force) => currentForce = _force;
        public void SetColor(Color _color) => renderer.material.color = _color;

        // Implement this OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, CurrentForce);
            Gizmos.DrawWireSphere(transform.position + CurrentForce, 0.1f);
        }

        // Implement this if you want to draw Gizmos only if the object is selected.
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;

            foreach (Vector3 direction in SteeringAgentHelper.DirectionsInCone(this, true))
            {
                Gizmos.DrawSphere(transform.position + direction, 0.1f);
            }
        }
    }
}