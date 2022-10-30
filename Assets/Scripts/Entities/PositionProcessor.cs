using UnityEngine;

namespace Entities
{
    public static class PositionProcessor
    {
        private const float MaxRayDistance = 6;
        private const int LayerWallIndex = 6;
        private const float YRayCastPosition = 0.05f;
        private const float SphereRadius = 5;
        private const float YSpawnPosition = 0.05f;

        public static Vector3 GetNewPosition()
        {
            Vector3 position = Random.insideUnitSphere * SphereRadius;
            position.y = YSpawnPosition;

            Vector3 raycastPosition = position;
            raycastPosition.y += YRayCastPosition;

            Ray ray = new Ray(raycastPosition, position - raycastPosition);

            if (Physics.Raycast(ray, MaxRayDistance, 1 << LayerWallIndex))
            {
                return GetNewPosition();
            }

            return position;
        }
    }
}