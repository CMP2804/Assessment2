using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace cmp2804.DistractionMechanic
{
    public struct DistractionSource
    {
        public Vector3 Origin { get; }
        private float _radius;

        public DistractionSource(Vector3 origin, float radius)
        {
            Origin = origin;
            _radius = radius;
            Debug.Log("radius = " + radius);
            EmitDistraction();
        }

        private void EmitDistraction()
        {
            Collider[] colliders = Physics.OverlapSphere(Origin, _radius);

            foreach (Collider collider in colliders)
            {
                IDistractable distractable = collider.GetComponent<IDistractable>();
                distractable?.Distract(this);
            }
        }
    }
}
